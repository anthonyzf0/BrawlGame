using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brawl.V4.Source.Server.Terrain
{
    class Map   //Server side map
    {
        //Server Map itself
        private Dictionary<int, Block> terrainData = new Dictionary<int, Block>();
        private int blockIndex = -1;

        public Map()
        {

        }

        //Gets a block
        public Block getBlock(int key)
        {
            return terrainData[key];
        }

        //Creates the inital map
        public void loadMap()
        {
            addBlock(new Vector3(3,0,3), new Vector3(0.5f, 0.5f, 0.5f));
        }
    
        //Adds a block to this map
        public int addBlock(Vector3 pos, Vector3 size)
        {
            blockIndex++;

            Block b = new Block(pos, size, blockIndex);

            //Adds block
            terrainData.Add(blockIndex, b);

            //Sends the data out to the clients
            EventController.addEvent("newBlock " + b.getBaseData());

            return blockIndex;

        }
        
        //Sends map to specific player
        public void sendMap(int client)
        {
            //Sends every blocks data to the client
            foreach (int key in terrainData.Keys) 
                EventController.addEvent(client, "newBlock " + terrainData[key].getBaseData());
            
        }

        //Syncs a specific block across the grid
        public void syncBlock(int id)
        {
            EventController.addEvent("adjust " + terrainData[id].getMainData());
        }

        //Updates the map
        public void update(float gameTime)
        {

        }

    }
}
