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
        private float speed = 3;
        private int jumps = 2;
        private float jumpPower = 7;
        
        //Id of local block that is the player
        public int blockId;

        private Vector3 lastPosition;

        public Player()
        {
        }

        //sets the block id
        public void setId(int blockId)
        {
            this.blockId = blockId;
        } 

        private Vector3 getMovement(Vector3 der)
        {
            return Camera.getDer(der, speed);
        }

        public void update(float t, Map map)
        {
            //If you arnt setup, dont bother
            if (blockId == -1) return;
            
            //Save last location
            lastPosition = map.getBlock(blockId).position;

            //Moves player
            Vector3 delta = Vector3.Zero;
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                delta -= getMovement(Vector3.Left);

            if (Keyboard.GetState().IsKeyDown(Keys.D))
                delta += getMovement(Vector3.Left);

            if (Keyboard.GetState().IsKeyDown(Keys.W))
                delta += getMovement(Vector3.Forward);

            if (Keyboard.GetState().IsKeyDown(Keys.S))
                delta -= getMovement(Vector3.Forward);

            //Jumps
            if (InputHandler.space)
                jump(map);

            //Adjust the value
            map.getBlock(blockId).impulse = delta;
            map.playerMovement(blockId, t);
            
            //Refresh jumps if you have landed
            if (map.getBlock(blockId).grounded)
                jumps = 2;
        }

        //Moves the camera to be on the block
        public void moveCamera(Map m) {

            Camera.camPosition = m.getBlock(blockId).position;
        }

        //Send the data to the server if need be
        public void sendData(Map map)
        {
            if (lastPosition != map.getBlock(blockId).position)
                ServerConnection.sendMessage("move", map.getBlock(blockId).position);

        }

        //When you jump
        private void jump(Map map)
        {
            if (jumps < 1) return;

            jumps--;

            map.getBlock(blockId).velocity.Y = jumpPower;

        }
        
        public void draw()
        {
            
        }

    }
}
