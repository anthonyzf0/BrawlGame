using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brawl.V4.Source.Menu.Gui
{
    class TextList
    {
        //Colors for selection
        private static Color defaultColor = Color.LightGray;
        private static Color selectColor = Color.Gray;

        private static int height = 30, width = 100;

        //Values
        private String[] values;
        private int selected = 0;
        
        public TextList(String[] vals)
        {
            values = vals;

        }

        //toggles selected index
        public String toggle()
        {
            selected++;

            if (selected == values.Length)
                selected = 0;

            return values[selected];
        }
        
        //Draw the rows and text
        public void draw(Point p, Render2D view)
        {
            for (int i = 0; i < values.Length; i++) 
            {
                int x = p.X + 30;
                int y = p.Y + i * height;

                if (i == selected)
                    view.drawBox(x, y, width, height-2, selectColor);
                else
                    view.drawBox(x, y, width, height-2, defaultColor);

                view.drawText(x + width/2, y + 15, values[i], Color.Black);

            }

        }

    }
}
