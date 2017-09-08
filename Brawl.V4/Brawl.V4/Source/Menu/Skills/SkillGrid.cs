using Brawl.V4.Source.Menu.Gui;
using Brawl.V4.Source.Menu.Skills.SkillConnection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brawl.V4.Source.Menu.Skills
{
    class SkillGrid
    {
        //Type of skillGrid
        public enum GridType { Projectile };
        private GridType gridType;

        //List of nodes
        private SkillGridCreator gridLoader;
        private List<SkillNode> nodes;
        private int currentConnection = -1;

        //Connections
        private List<NodeConnection> connections = new List<NodeConnection>();
        private NodeConnection mouseConnection;

        //Stars
        private Random rand = new Random();
        private int starCount = 400;
        private Vector2 starSize = new Vector2(800, 700);
        private List<Vector2> points = new List<Vector2>();

        //Mouse click
        private bool lastMouseClick = false;

        public SkillGrid(GridType type)
        {
            //Loads the nodes
            gridLoader = new SkillGridCreator();

            gridType = type;
            nodes = gridLoader.getGrid(gridType);

            //Connections
            mouseConnection = new NodeConnection(-1, -1, Vector2.Zero, new Vector2(300, 300), true);

            //GenStars
            for (int i = 0; i < starCount; i++)
                points.Add(new Vector2((float)rand.NextDouble() * starSize.X, (float)rand.NextDouble() * starSize.Y));

        }

        //Adds a line from connection to i
        private void addConnection(int i)
        {
            foreach (NodeConnection conn in connections)
                if (conn.isSame(currentConnection, i))
                    return;
                
            connections.Add(new NodeConnection( currentConnection, i,
                                                nodes[currentConnection].getCenter(),
                                                nodes[i].getCenter()));

            currentConnection = -1;
        }

        //Adjusts the mouse line to be correct
        private void adjustMouseLine()
        {
            if (currentConnection == -1) return; 

            mouseConnection.calculateAngle(nodes[currentConnection].getCenter(),
                                            Mouse.GetState().Position.ToVector2());
        }
        
        public void update()
        {
            //If your right click wasnt wasted
            bool didTrigger = false;
            
            //Update nodes & register connections
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].update())
                    didTrigger = true;

                if (nodes[i].clicked)
                {
                    if (currentConnection == -1)
                        currentConnection = i;
                    else
                        addConnection(i);

                    return;
            } }

            //Update connections
            foreach (NodeConnection c in connections)

                //Returns true if the node has been killed
                if (c.update())
                {
                    connections.Remove(c);
                    break;
                }

            //If you right click and you didnt trigger a swap on a node
            bool thisClick = Mouse.GetState().RightButton == ButtonState.Pressed;

            if (thisClick && !lastMouseClick && !didTrigger)
                currentConnection = -1;

            lastMouseClick = thisClick;

            mouseConnection.update();
        }

        //Gets the code from a point i on the grid
        private String getChildrenCode(int i)
        {
            String code = nodes[i].currentValue + "[";
            foreach (NodeConnection c in connections)
                if (c.nodeA == i)
                    code += getChildrenCode(c.nodeB);

            return code + "]";
        }

        //Gets a code from the start of the grid
        public String generateCode()
        {
            String code = "";

            for(int i = 0; i < nodes.Count; i++) { 

                bool noParent = true;

                //If no node has a child of this node, then it is good
                foreach (NodeConnection other in connections)
                    if (other.nodeB == i)
                        noParent = false;

                if (noParent)
                    code += getChildrenCode(i);

            }

            return code;
        }

        public void draw(Render2D view)
        {
            //Makes the mouse line ready to draw
            adjustMouseLine();

            foreach (Vector2 v in points)
                view.drawBox((int)v.X, (int)v.Y, 2, 2, Color.White);

            //Draw node connections
            foreach (NodeConnection c in connections)
                c.draw(view);

            //If you are tring to connect a node, draw the mouse line
            if (currentConnection!=-1)
                mouseConnection.draw(view);

            //Draw node backgrounds
            foreach (SkillNode n in nodes)
                n.drawBackground(view);

            //Draw node forgrounds
            foreach (SkillNode n in nodes)
                n.drawForeground(view);

        }

    }
}
