using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brawl.V4.Source.Server
{
    class EventController
    {
        private static List<String> events = new List<string>();            //Events to everyone
        private static List<String> specificEvents = new List<string>();    //Events to one person 

        public EventController()
        {

        }

        //Add events
        public static void addEvent(int client, String e)
        {
            specificEvents.Add(client + e);
        }
        public static void addEvent(String e)
        {
            events.Add(e);
        }

        public void sendEvents(List<Client> clients)
        {
            //Sends to all clients
            for (int i = 0; i < events.Count;)
            {
                foreach (Client c in clients)
                    c.sendData(events[0]);
                events.RemoveAt(0);
            }

            //Sends the data to one client specificly
            for (int i = 0; i < specificEvents.Count;)
            {

                String e = specificEvents[i];

                int index = Convert.ToInt32(e[0] + "");
                clients[index].sendData(e.Substring(1));

                specificEvents.RemoveAt(0);

            }
        }

    }
}
