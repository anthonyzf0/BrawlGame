using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Brawl.V4.Source.Server
{
    class ServerController
    {
        //Server
        private Server server;

        //Threads to run server
        private Thread syncDataThread, updateMapThread, recieveDataThread;

        //Event controller
        private EventController events;
        
        public ServerController()
        {
            //Creates server
            server = new Server();

            events = new EventController();

            //Thread Setup
            syncDataThread = new Thread(new ThreadStart(syncData));
            updateMapThread = new Thread(new ThreadStart(updateMap));
            recieveDataThread = new Thread(new ThreadStart(recieveData));

        }
        
        //Called to start the server
        public void startServer()
        {
            server.start();

            //Starts server threads
            syncDataThread.Start();
            updateMapThread.Start();
            recieveDataThread.Start();
        }

        //Sends data of all the server changes to the clients connected
        private void syncData()
        {
            while (true)
            {
                //Sends all the events
                server.sendEventData(events);
            }
        }

        //Updates the map data to make sure everything is running fine
        private void updateMap()
        {
            //Keeps track of updates
            Stopwatch gameTimer = new Stopwatch();
            gameTimer.Start();

            while (true)
            {
                //Update gameTimer
                float time = (float)gameTimer.Elapsed.TotalSeconds;
                gameTimer.Restart();

                server.updateMap(time); 
            }

        }

        //Get data from clients & check connections
        private void recieveData()
        {
            while (true)
            {
                //Checks for clients and reads their messages
                server.checkClientConnections();
                server.readClientMessages();
            }
        }

    }
}
