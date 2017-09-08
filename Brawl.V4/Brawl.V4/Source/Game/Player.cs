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

        private Vector3 getMovement(Vector3 der, float deltaTime)
        {
            return Camera.getDer(der, speed * deltaTime);
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
                delta -= getMovement(Vector3.Left, t);

            if (Keyboard.GetState().IsKeyDown(Keys.D))
                delta += getMovement(Vector3.Left, t);

            if (Keyboard.GetState().IsKeyDown(Keys.W))
                delta += getMovement(Vector3.Forward, t);

            if (Keyboard.GetState().IsKeyDown(Keys.S))
                delta -= getMovement(Vector3.Forward, t);

            //Jumps
            if (InputHandler.space)
                jump(map);
            
            //Adjust the value
            delta = map.playerMovement(blockId, delta);

            //Move block
            map.getBlock(blockId).position += delta;
            
            Camera.camPosition = map.getBlock(blockId).position;

            //Refresh jumps if you have landed
            if (map.getBlock(blockId).grounded)
                jumps = 2;
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
