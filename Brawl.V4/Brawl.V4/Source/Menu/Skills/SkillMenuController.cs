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
        //All the buttons used
        private Dictionary<String, Button> buttons = new Dictionary<string, Button>();

        //MainCallback to tell main what happened here
        Func<string, int> callback;

        //The grid itself
        private SkillGrid skillgGrid;

        public SkillMenuController(Func<String, int> callback)
        {
            //Sets up callback
            this.callback = callback;
            createButton("back", 950, 720, "Back");

            //Creates a projectile grid
            skillgGrid = new SkillGrid(SkillGrid.GridType.Projectile);
        }

        private void createButton(String name, int x, int y, String text)
        {
            buttons.Add(name, new Button(text, x, y, 200, 50, "button"));
        }

        public void update()
        {
            //Buttons updates
            if (buttons["back"].left)
                callback("main");

            //Skill grid
            skillgGrid.update();

        }

        public void draw(Render2D view)
        {
            //Draw buttons
            buttons["back"].draw(view);

            //Skill grid
            skillgGrid.draw(view);
        }


    }
}
