using Brawl.V4.Source.Menu.Gui;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brawl.V4.Source.Menu.Skills.SkillConnection
{
    class ConnectionLine
    {
        private static Random rand = new Random();

        //location
        private Vector2 a, b;
        private Vector2 loc, speed;

        //Info
        private Color c;
        private int size;

        public ConnectionLine(Vector2 a, Vector2 b)
        {

            this.a = a;
            this.b = b;

            //Speed and location
            speed = (b - a)/100 * (float)rand.NextDouble();
            loc = a + speed * rand.Next(10);

            c = new Color(0, 0, 60, (float)rand.NextDouble());

            size = rand.Next(3)+1;

        }

        public bool update()
        {
            loc += speed;

            //Returns if it should kill this
            return Vector2.Distance(loc, b) < 20;

        }

        public void draw(Render2D view)
        {
            view.drawBox((int)loc.X, (int)loc.Y, size, size, c);
        }

    }
}
