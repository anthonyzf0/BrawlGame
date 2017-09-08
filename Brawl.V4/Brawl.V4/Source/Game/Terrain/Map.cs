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

        public Map()
        {
            terrainMap = new Dictionary<int, Block>();
        }
        
        //Ways to create a block
        public void createBlock(int id, Vector3 pos, Vector3 size)
        {
            terrainMap.Add(id, new Block(pos, size));
        }

        public Block getBlock(int key)
        {
            return terrainMap[key];
        }
       
        public void update(float gameTime)
        {
            //Updates blocks
            foreach (int key in terrainMap.Keys)
                terrainMap[key].update(gameTime);
        }

        public void draw(Render3D view)
        {
            //Draws blocks
            foreach (int key in terrainMap.Keys)
                terrainMap[key].draw(view);
        }

    }
}
