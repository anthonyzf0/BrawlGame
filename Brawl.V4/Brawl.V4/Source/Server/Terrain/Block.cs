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
        public Vector3 location, size;

        //Server data
        private int index;

        public Block(Vector3 location, Vector3 size, int index)
        {

            this.location = location;
            this.size = size;

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
            return index + " " + prepairVector(location) + " " + prepairVector(size);
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
