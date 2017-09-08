using Brawl.V4.Source.Game.Network;
using Brawl.V4.Source.Game.Terrain;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brawl.V4.Source.Game
{
    class Player
    {
        //Stats
        private float speed = 2;
        
        private int blockId;

        public Player()
        {
        }

        //sets the block id
        public void setId(int blockId)
        {
            this.blockId = blockId;
        } 

        private Vector3 getMovement(Vector3 der, float deltaTime)
        {
            return Camera.getDer(der, speed * deltaTime);
        }

        public void update(float t, Map map)
        {
            //If you arnt setup, dont bother
            if (blockId == -1) return;

            Vector3 delta = Vector3.Zero;
            //TODO change
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                delta -= getMovement(Vector3.Left, t);

            if (Keyboard.GetState().IsKeyDown(Keys.D))
                delta += getMovement(Vector3.Left, t);

            if (Keyboard.GetState().IsKeyDown(Keys.W))
                delta += getMovement(Vector3.Forward, t);

            if (Keyboard.GetState().IsKeyDown(Keys.S))
                delta -= getMovement(Vector3.Forward, t);

            if (delta != Vector3.Zero)
            {
                //Move block
                map.getBlock(blockId).position += delta;
                Camera.camPosition = map.getBlock(blockId).position;

                ServerConnection.sendMessage("move", map.getBlock(blockId).position);
            }
        }

        public void draw()
        {


        }

    }
}
