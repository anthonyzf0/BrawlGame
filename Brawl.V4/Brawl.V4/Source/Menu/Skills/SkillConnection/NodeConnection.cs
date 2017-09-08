using Brawl.V4.Source.Menu.Gui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brawl.V4.Source.Menu.Skills.SkillConnection
{
    class NodeConnection
    {
        //Colors
        private static Color normal = new Color(0, 0, 60);
        private static Color hover = new Color(60, 0, 0);

        //Distance needed to remove the connection
        private static float removeDistance = 20;
        private static Random rand = new Random();

        //Connections
        public int nodeA, nodeB;
        private Vector2 aLoc, bLoc;
        private Vector2 middle;

        //Angle to draw at
        private double rotation;
        private float distance;

        //Other info
        public bool dead = false;
        private bool glow = false;
        private bool showLine = false;

        //Connection line
        private List<ConnectionLine> blocks = new List<ConnectionLine>();

        public NodeConnection(int a, int b, Vector2 aLoc, Vector2 bLoc, bool draw = false)
        {
            //Node locations and ids
            nodeA = a;
            nodeB = b;

            this.aLoc = aLoc;
            this.bLoc = bLoc;

            //Calculates the rotation needed to draw
            calculateAngle(aLoc, bLoc);

            showLine = draw;
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

            //Middle of line
            middle = aLoc + new Vector2((float)(distance / 2 * Math.Cos(rotation)), (float)(distance / 2 * Math.Sin(rotation)));

            //Clears blocks
            blocks.Clear();
        }

        //Returns if a to b is not the same as this current connections
        public bool isSame(int a, int b)
        {
            return (a == nodeA && b == this.nodeB) || (a == nodeB && b == this.nodeA);
        }
        
        //updateConnection
        public bool update()
        {
            Vector2 mouse = Mouse.GetState().Position.ToVector2();

            //Remove connection
            if (Vector2.Distance(mouse, middle) < removeDistance)
            {
                if (InputHandler.leftClick)
                    dead = true;
                glow = true;
            }
            else
                glow = false;

            //Update blocks
            for (int i = 0; i < blocks.Count; i++)
                if (blocks[i].update())
                    blocks.RemoveAt(i);

            //Add new block
            if (rand.NextDouble() < 0.3)
                blocks.Add(new ConnectionLine(aLoc, bLoc));
            
            return dead;
        }

        //Draws the line
        public void draw(Render2D view)
        {
            //If you highlight it
            if (glow)
                view.drawBox((int)aLoc.X, (int)aLoc.Y - 2, (int)distance, 5, hover, rotation);
            else if (showLine)
                view.drawBox((int)aLoc.X, (int)aLoc.Y - 2, (int)distance, 5, normal, rotation);

            //Draw boxes
            for (int i = 0; i < blocks.Count; i++)
                blocks[i].draw(view);
        }
    }
}
