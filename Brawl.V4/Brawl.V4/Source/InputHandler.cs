using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brawl.V4.Source
{
    class InputHandler
    {
        //Mouse
        public static bool leftClick = false, rightClick = false;
        private static bool lastLeft = false, lastRight = false;

        public static bool space = false;
        private static bool lastSpace = false;

        public InputHandler()
        {


        }

        //Updates left and right clicks
        public void update()
        {
            //Mouse
            bool thisRight = Mouse.GetState().RightButton == ButtonState.Pressed;
            bool thisLeft = Mouse.GetState().LeftButton == ButtonState.Pressed;
            
            leftClick = !lastLeft && thisLeft;
            lastLeft = thisLeft;

            rightClick = !lastRight && thisRight;
            lastRight = thisRight;

            bool thisSpace = Keyboard.GetState().IsKeyDown(Keys.Space);

            space = !lastSpace && thisSpace;
            lastSpace = thisSpace;

        }

    }

}
