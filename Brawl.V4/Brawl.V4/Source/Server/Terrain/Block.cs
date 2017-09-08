using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brawl.V4.Source.Server.Terrain
{
    class Block
    {

        //Position data
        public Vector3 location, size, velocity, acceleration;

        //Server data
        private int index;
        private String tag;

        public Block(Vector3 location, Vector3 size, Vector3 velocity, Vector3 acceleration, int index, String tag)
        {

            this.location = location;
            this.size = size;
            this.velocity = velocity;
            this.acceleration = acceleration;

            this.tag = tag;

            this.index = index;

        }
        
        //Updates the block
        public void update(float gameTime)
        {
        }
        
        //Prepairs a vector for being sent
        private static String prepairVector(Vector3 v)
        {
            return Math.Round(v.X, 4) + " " + Math.Round(v.Y, 4) + " " + Math.Round(v.Z, 4);
        }

        //Gets data ready to send to clients
        public String getBaseData()
        {
            return index + " " + prepairVector(location) + " " + prepairVector(size) + " " + prepairVector(velocity) + " " + prepairVector(acceleration) + " " + tag;
        }
        public String getMainData()
        {
            return index + " " + prepairVector(location);
        }
        public String getLocationData()
        {
            return prepairVector(location);
        }

    }
}
