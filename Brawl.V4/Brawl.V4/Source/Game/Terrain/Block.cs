using Brawl.V4.Source.Game.Render;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brawl.V4.Source.Game.Terrain
{
    class Block
    {
        //Position info
        public Vector3 position, size;

        public Block(Vector3 position, Vector3 size)
        {

            this.position = position;
            this.size = size;
        }

        public void update(float gameTime)
        {

        }

        public void draw(Render3D render)
        {
            //Draw me
            render.draw("MonoCube", position, size);

        }
    }
}
