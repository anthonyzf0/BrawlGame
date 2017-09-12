using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brawl.V4.Source.Game.Terrain
{
    class Collision
    {
        private List<String> collideableObjects;

        public Collision()
        {
            collideableObjects = new List<string>() { "terrain" };

        }

        //Will a hit b if a was at pos
        private bool hit(Vector3 aPos, Block a, Block b)
        {
            return (Math.Abs(aPos.X - b.position.X) < (a.size.X + b.size.X) &&
                    Math.Abs(aPos.Y - b.position.Y) < (a.size.Y + b.size.Y) &&
                    Math.Abs(aPos.Z - b.position.Z) < (a.size.Z + b.size.Z));

        }
        
        //Moves block a to not hit things
        public void adjustBlock(Block a, Dictionary<int, Block> map, float gameTime)
        {
            //Gets the next position of "A"
            Vector3 pos = a.getNextPosition(gameTime);

            //Movement in each derection
            Vector3 x = new Vector3(pos.X, 0, 0) + a.position;
            Vector3 y = new Vector3(0, pos.Y, 0) + a.position;
            Vector3 z = new Vector3(0, 0, pos.Z) + a.position;

            foreach (int key in map.Keys)
            {
                //If its the player block, or not a terrain block, skip it
                if (a==map[key]) continue;
                if (!collideableObjects.Contains(map[key].tag)) continue;

                if (hit(y, a, map[key]))
                {
                    a.stop(new Vector3(1, 0, 1));
                    
                }
                
                else if (hit(x, a, map[key]))
                {
                    a.stop(new Vector3(0, 1, 1));
                    
                }
                
                else if (hit(z, a, map[key]))
                {
                    a.stop(new Vector3(1, 1, 0));
                   
                }
            }
            
        }

    }
}
