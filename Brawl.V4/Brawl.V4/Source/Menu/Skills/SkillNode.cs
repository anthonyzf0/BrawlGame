using Brawl.V4.Source.Menu.Gui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brawl.V4.Source.Menu.Skills
{
    class SkillNode
    {
        private static int worldScale = 20;
        private static int nodeSize = 64;

        //Info on this node
        private int x, y, centerX, centerY;
        private String[] values;
        public bool clicked = false;

        //The button for this node & its graphics
        private Button button;
        private TextList list;
        private Sprite nodeImage;
        private String currentValue;

        public SkillNode(int xloc, int yloc, String[] values)
        {
            //Location data
            x = xloc * worldScale;
            y = yloc * worldScale;
            centerX = x + nodeSize / 2;
            centerY = y + nodeSize / 2;

            //Node values
            this.values = values;
            list = new TextList(values);
            currentValue = values[0];

            //Create button for the click & hover
            button = new Button(x, y, nodeSize, nodeSize);
            nodeImage = new Sprite(centerX, centerY, nodeSize, "Node");

        }

        public bool update()
        {
            //Update node sprite
            nodeImage.update();

            //When hovering scale node
            if (button.hover)
                nodeImage.scale(nodeSize*2);
            else
                nodeImage.scale(nodeSize);

            //Click to toggle list
            if (button.right)
                currentValue = list.toggle();

            //Register clicks
            clicked = button.left;

            return button.right;
        }

        //Gets the nodes center for connection reasons
        public Vector2 getCenter()
        {
            return new Vector2(centerX, centerY);
        }
        
        //Draw image
        public void drawBackground(Render2D view)
        {
            nodeImage.draw(view);
        }

        public void drawForeground(Render2D view)
        {
            //List of options
            if (button.hover)
                list.draw(Mouse.GetState().Position, view);

            //Text
            view.drawText(centerX, centerY, currentValue, Color.White);
        }

    }
}
