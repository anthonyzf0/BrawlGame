using Brawl.V4.Source.Game.Terrain;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Brawl.V4.Source.Game.Network
{
    class ServerConnection
    {
        //Connection info
        private int port = 13000;
        private TcpClient client;
        private NetworkStream stream;

        //Server connections
        private static List<String> msgs = new List<string>();
        private static Byte[] bytes = new Byte[128];


        private int clientBlock = -1;

        public ServerConnection()
        {
            //Connect to the server
            client = new TcpClient("localhost", port);
            stream = client.GetStream();
        }

        //Queues a msg to be sent to the server
        public static void sendMessage(String s, Vector3 v)
        {
            msgs.Add(s + " " + prepairVector(v));
        }

        //Prepairs a vector for being sent
        private static String prepairVector(Vector3 v)
        {
            return Math.Round(v.X, 4) + " " + Math.Round(v.Y, 4) + " " + Math.Round(v.Z, 4);
        }

        //Sends all those messages
        public void sendQueuedMsgs()
        {

            foreach (String msg in msgs)
            {
                Byte[] data = System.Text.Encoding.ASCII.GetBytes("|" + msg + "|");
                stream.Write(data, 0, data.Length);
            }
            msgs.Clear();
        }

        //Gets all the commands the server has sent
        public String[] getServerCommands()
        {
            String readData = "";

            while (stream.DataAvailable)
            {
                int d = stream.Read(bytes, 0, bytes.Length);
                String buffer = Encoding.ASCII.GetString(bytes, 0, d);

                readData += buffer;

            }
            
            if (readData == "") return null;

            return readData.Split('|');
        }

        //Gets vector from data
        private Vector3 recieveVector(String[] data, int index)
        {
            return new Vector3((float)Convert.ToDouble(data[index]),
                                (float)Convert.ToDouble(data[index+1]),
                                (float)Convert.ToDouble(data[index+2]));
        }

        //Get messages from the server
        public void recieveMsgs(Map map, Player player)
        {
            //Gets the server commands
            String[] serverData = getServerCommands();
            if (serverData == null) return;

            foreach (String data in serverData)
            {
                if (data == "") continue;

                //Splits data by spaces to get parts of command
                String[] command = data.Split(' ');

                //The second term of every command is the id
                int id = Convert.ToInt32(command[1]);

                switch (command[0])
                {
                    case "newBlock":    //newBlock [id] [position] [size] [velocity] [acceleration] [tag]

                        Vector3 position = recieveVector(command, 2);
                        Vector3 size = recieveVector(command, 5);
                        Vector3 velocity = recieveVector(command, 8);
                        Vector3 acceleration = recieveVector(command, 11);

                        map.createBlock(id, position, size, velocity, acceleration, command[14]);

                        break;

                    case "init":

                        if (clientBlock != -1) break;

                        //Sets the id of the client block
                        clientBlock = id;
                        player.setId(clientBlock);

                        break;

                    case "adjust":

                        if (id == clientBlock) break;

                        map.getBlock(id).position = recieveVector(command, 2);

                        break;

                }
            }
        }

    }
}
