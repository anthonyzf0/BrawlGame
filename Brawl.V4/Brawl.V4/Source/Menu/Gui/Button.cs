using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brawl.V4.Source.Menu.Gui
{
    class Button
    {
        //All game buttons
        private static List<Button> buttons = new List<Button>();

        //InputValues
        public bool right, lastRight;
        public bool left, lastLeft;
        public bool hover;

        //Location and image
        private Rectangle rect;
        private string imageName;
        private String text;
        private int textX;

        private bool drawImage = true;

        public Button(int x, int y, int width, int height)
        {
            //Rectangle
            rect = new Rectangle(x, y, width, height);

            drawImage = false;

            buttons.Add(this);
        }

        public Button(String text, int x, int y, int width, int height, String imageName)
        {
            this.text = text;
            this.imageName = imageName;
            rect = new Rectangle(x, y, width, height);

            int textSpace = text.Length * 10;
            textX = x + width / 2 - textSpace / 2;

            buttons.Add(this);
        }

        //Called to update values
        public void leftClick(bool value)
        {
            left = value && hover && !lastLeft;
            lastLeft = value && hover;
        }
        public void rightClick(bool value)
        {
            right = value && hover && !lastRight;
            lastRight = value && hover;
        }

        public void checkHover(Point p)
        {
            hover = rect.Contains(p);
        }

        //Updates all buttons
        public static void update()
        {
            //Get mouse info
            MouseState mouse = Mouse.GetState();
            Point p = mouse.Position;

            bool left = mouse.LeftButton == ButtonState.Pressed;
            bool right = mouse.RightButton == ButtonState.Pressed;

            foreach (Button b in buttons)
            {
                //Checks if the mouse is over the button
                b.checkHover(p);

                //Checks right / left click
                b.leftClick(left);
                b.rightClick(right);
            }
               
        }
        
        public void draw(Render2D renderer)
        {
            if (!drawImage) return;

            renderer.drawTexture(imageName, rect);
            renderer.drawText(text, textX, rect.Top+rect.Height/2 - 10);
        }
        
    }
}
