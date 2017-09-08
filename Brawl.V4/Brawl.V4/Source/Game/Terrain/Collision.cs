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
            return (Math.Abs(aPos.X - b.position.X) < a.size.X + b.size.X &&
                    Math.Abs(aPos.Y - b.position.Y) < a.size.Y + b.size.Y &&
                    Math.Abs(aPos.Z - b.position.Z) < a.size.Z + b.size.Z);

        }

        //Moves block a to not hit things
        public Vector3 adjustBlock(Block a, Vector3 movement, Dictionary<int, Block> map)
        {
            //Gets the next position of "A"
            a.impulse = movement;
            Vector3 pos = a.getNextPosition();

            //Movement in each derection
            Vector3 x = new Vector3(pos.X, 0, 0) + a.position;
            Vector3 y = new Vector3(0, pos.Y, 0) + a.position;
            Vector3 z = new Vector3(0, 0, pos.Z) + a.position;

            foreach (int key in map.Keys)
            {
                //If its the player block, or not a terrain block, skip it
                if (a==map[key]) continue;
                if (!collideableObjects.Contains(map[key].tag)) continue;

                //If you would hit somewhere, then dont go that way
                if (hit(x, a, map[key]))
                {
                    movement.X = 0;
                    a.velocity.X = 0;
                }
                if (hit(y, a, map[key]))
                {
                    movement.Y = 0;
                    a.ground();
                }
                if (hit(z, a, map[key]))
                {
                    movement.Z = 0;
                    a.velocity.Z = 0;
                }
            }

            return movement;

        }

    }
}
