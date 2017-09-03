using Brawl.V4.Source.Menu.Gui;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brawl.V4.Source.Menu.Skills
{
    class NodeConnection
    {
        //Connections
        private int nodeA, nodeB;
        private Vector2 aLoc, bLoc;

        //Angle to draw at
        private double rotation;
        private float distance;

        public NodeConnection(int a, int b, Vector2 aLoc, Vector2 bLoc)
        {
            //Node locations and ids
            nodeA = a;
            nodeB = b;

            this.aLoc = aLoc;
            this.bLoc = bLoc;

            //Calculates the rotation needed to draw
            calculateAngle(aLoc, bLoc);
        }

        //Calculates angle & distance
        public void calculateAngle(Vector2 aLoc, Vector2 bLoc)
        {
            this.aLoc = aLoc;
            this.bLoc = bLoc;

            //Distance
            distance = Vector2.Distance(aLoc, bLoc);

            //Rotation
            rotation = Math.Atan((bLoc.Y - aLoc.Y) / (bLoc.X - aLoc.X));
            if (bLoc.X < aLoc.X) rotation += Math.PI;

        }

        //Returns if a to b is not the same as this current connections
        public bool isSame(int a, int b)
        {
            return (a == nodeA && b == this.nodeB) || (a == nodeB && b == this.nodeA);
        }

        public void update()
        {

        }

        //Draws the line
        public void draw(Render2D view)
        {
            view.drawBox((int)aLoc.X, (int)aLoc.Y - 2, (int)distance, 5, Color.Black, rotation);
        }

    }
}
