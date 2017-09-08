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
        private static float averageTimestep = 0.04f;
        
        //Position info
        public Vector3 position, size, velocity, acceleration, impulse;

        //Values
        public bool grounded = false;

        public String tag;

        public Block(Vector3 position, Vector3 size, Vector3 velocity, Vector3 acceleration, String tag)
        {

            this.position = position;
            this.size = size;
            this.velocity = velocity;
            this.acceleration = acceleration;

            this.tag = tag;

        }

        //next position, used for collision
        public Vector3 getNextPosition()
        {
            return position + (impulse + velocity) * averageTimestep;

        }

        public void ground()
        {
            velocity.Y = 0;
            grounded = true;
        }

        public void update(float gameTime)
        { 
            grounded = false;

            position += velocity * gameTime;
            velocity += acceleration * gameTime;
        }

        public void draw(Render3D render)
        {
            //Draw me
            render.draw("MonoCube", position, size);

        }
    }
}
