using Brawl.V4.Source.Game.Network;
using Brawl.V4.Source.Game.Render;
using Brawl.V4.Source.Game.Terrain;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brawl.V4.Source.Game
{
    class GameController
    {
        //Renderer
        private Render3D render;

        //Components
        private Map map;
        private ServerConnection network;
        private Player player;

        //GameTimer
        private Stopwatch gameTime;

        public GameController(GraphicsDeviceManager graphics)
        {
            //Empty map
            map = new Map();

            //Camera & non important initialization
            Camera.initialize(graphics);
            gameTime = new Stopwatch();
        }

        public void connect(ContentManager content)
        {
            //Initialize important components when you connect
            render = new Render3D(content);
            player = new Player();
            
            //Connects to the server
            network = new ServerConnection();
        }

        public void update()
        {
            //Keep track of game time
            float deltaTime = (float)gameTime.Elapsed.TotalSeconds;
            gameTime.Restart();

            //Gets server commands
            network.recieveMsgs(map, player);

            //Update game itself
            player.update(deltaTime, map);
            map.update(deltaTime, player);

            //After everything is updated, send the player data to the server
            player.moveCamera(map);
            player.sendData(map);

            //Sends all the messages queued to be sent
            network.sendQueuedMsgs();
        }

        //Draws the game
        public void draw()
        {
            //Smooth camera before drawing
            Camera.update();

            //Draws map
            map.draw(render);
            
        }

    }
}
