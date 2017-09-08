using Brawl.V4.Source.Server.Terrain;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Brawl.V4.Source.Server
{
    class Client
    {
        //Default block size
        private static Vector3 size = new Vector3(0.2f, 0.4f, 0.2f);

        //Network data
        private NetworkStream stream;
        private Byte[] bytes;

        //The block id assigned to this client
        public int blockId;

        public Client (NetworkStream connection)
        {
            stream = connection;
        }

        public void init(Map m)
        {
            //TODO add some sort of spawnPoint
            blockId = m.addBlock(Vector3.Zero, size);
            EventController.addEvent("init " + blockId);

        }
        
        //Sends data out to client
        public void sendData(String data)
        {
            bytes = Encoding.ASCII.GetBytes("|" + data + "|");
            stream.Write(bytes, 0, bytes.Length);
        }
        
        //Reads the next set of commands from the client
        private string readData()
        {
            if (!stream.DataAvailable) return "";

            bytes = new Byte[1024];
            int i = stream.Read(bytes, 0, bytes.Length);
            return (Encoding.ASCII.GetString(bytes, 0, i));
        }


        //Updates the client
        public void update(Map map)
        {
            //Sends the data to be interpreted
            String[] data = readData().Split('|');
            foreach (String s in data)
                if (s != "")
                    interpretData(s.Split(' '), map);

        }

        //Gets a vector from teh client data at intdex
        private Vector3 getVector(String[] cmd, int index)
        {
            return new Vector3( (float)Convert.ToDouble(cmd[index]),
                                (float)Convert.ToDouble(cmd[index+1]),
                                (float)Convert.ToDouble(cmd[index+2]));
        }

        //Interprets the data from the client
        private void interpretData(String[] cmd, Map map)
        {
            switch (cmd[0]) {

                case "move":

                    //Sets the block position
                    map.getBlock(blockId).location = getVector(cmd, 1);

                    //Tells the map to sync the data to the clients
                    map.syncBlock(blockId);

                    break;

            }
            
        }

    }
}
