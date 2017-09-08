using Brawl.V4.Source.Server.Terrain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Brawl.V4.Source.Server
{
    class Server
    { 
        //Server values
        private int port = 13000;
        private TcpListener server;
        private IPAddress localAddr;

        //All connected clients
        private List<Client> clients;

        //Server components
        private Map map;
    
        public Server()
        {
            //Server
            clients = new List<Client>();
            localAddr = IPAddress.Parse("127.0.0.1");
          
            //Create server object
            server = new TcpListener(localAddr, port);

        }
        
        //Starts the server
        public void start()
        {
            //Creates map
            map = new Map();

            server.Start();

            //Loads the map and sends it out
            map.loadMap();
        }

        //Updates map
        public void updateMap(float gameTime)
        {
            map.update(gameTime);

        }

        //Sees if anything is trying to connect to the server
        public void checkClientConnections()
        {
            //If someone is pending
            if (server.Pending())
            {
                TcpClient client = server.AcceptTcpClient();

                //Creact a client for our connection
                Client c = new Client(client.GetStream());
                 clients.Add(c);
                
                //Setup map data and sends it to clients
                map.sendMap(clients.Count - 1);
                c.init(map);

            }

        }

        //Sends the events
        public void sendEventData(EventController events)
        {
            events.sendEvents(clients);
        }

        public void readClientMessages()
        {
            //Updates each client
            for (int i = 0; i < clients.Count; i++)
                clients[i].update(map);

        }

    }
}
