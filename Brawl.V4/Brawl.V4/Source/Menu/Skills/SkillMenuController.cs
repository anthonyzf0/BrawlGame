using Brawl.V4.Source.Menu.Gui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brawl.V4.Source.Menu.Skills
{
    class SkillMenuController
    {
        private Dictionary<String, Button> buttons = new Dictionary<string, Button>();

        //MainCallback to tell main what happened here
        Func<string, int> callback;
        
        public SkillMenuController(Func<String, int> callback)
        {
            this.callback = callback;

            createButton("back", 950, 720, "Back");
        }

        private void createButton(String name, int x, int y, String text)
        {
            buttons.Add(name, new Button(text, x, y, 200, 50, "button"));
        }

        public void update()
        {
            if (buttons["back"].left)
                callback("main");

        }

        public void draw(Render2D view)
        {
            buttons["back"].draw(view);
        }


    }
}
