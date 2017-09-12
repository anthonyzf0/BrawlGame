using Brawl.V4.Source.Game.Render;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brawl.V4.Source.Game.Terrain
{
    class Map   //Client side map
    {

        //All game blocks sorted by [id]
        private Dictionary<int, Block> terrainMap;

        //Collision checks
        private Collision collision;

        public Map()
        {
            terrainMap = new Dictionary<int, Block>();
            collision = new Collision();
        }
        
        //Ways to create a block
        public void createBlock(int id, Vector3 pos, Vector3 size, Vector3 velocity, Vector3 acceleration, String tag)
        {
            terrainMap.Add(id, new Block(pos, size, velocity, acceleration, tag));
        }
        public void createBlock(int id, Vector3 pos, Vector3 size)
        {
            createBlock(id, pos, size, Vector3.Zero, Vector3.Zero, "terrain");
        }

        public Block getBlock(int key)
        {
            return terrainMap[key];
        }

        //Check player movement and return what is valid
        public void playerMovement(int id, float gameTime)
        {
            //Has the collision file adjust the amount to move the player so they dont hit anything
            collision.adjustBlock(terrainMap[id], terrainMap, gameTime);
        }
        
        public void update(float gameTime, Player p)
        {
            //Updates blocks
            foreach (int key in terrainMap.Keys)
            {
                //Dont update other players
                if (key != p.blockId && terrainMap[key].tag == "player") continue;

                terrainMap[key].update(gameTime);
            }
        }

        public void draw(Render3D view)
        {
            //Draws blocks
            foreach (int key in terrainMap.Keys)
                terrainMap[key].draw(view);
        }

    }
}
