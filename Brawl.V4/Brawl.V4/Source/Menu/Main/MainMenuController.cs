using Brawl.V4.Source.Menu.Gui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brawl.V4.Source.Menu.Main
{
    class MainMenuController
    {
        //All the buttons
        private Dictionary<String, Button> buttons = new Dictionary<string, Button>();

        //Menu State
        private enum MenuState {Main, Options, Credits, HostSetup };
        private MenuState menuState = MenuState.Main;

        //MenuLists
        private Dictionary<MenuState, List<String>> menuButtons = new Dictionary<MenuState, List<string>>();

        //MainCallback to tell main what happened here
        Func<string, int> callback;

        public MainMenuController(Func<string, int> callback)
        {
            this.callback = callback;

            //Create buttons
            createButton("host", "Host A Game", 230);   //TODO add this
            createButton("join", "Join A Game", 300);   //TODO add this
            createButton("skill", "Setup Skills", 370);
            createButton("options", "Options", 440);
            createButton("credits", "Credits", 510);
            createButton("back", "Back", 580);

            //Sets what buttons are shown when
            menuButtons.Add(MenuState.Main, new List<string> { "host", "join", "skill", "options", "credits" });
            menuButtons.Add(MenuState.Credits, new List<string> { "back" });
            menuButtons.Add(MenuState.Options, new List<string> { "back" });

        }

        private void createButton(String name, String text, int y)
        {
            buttons.Add(name, new Button(text, 500, y, 200, 50, "button"));
        }

        public void update()
        {
            switch (menuState)
            {
                case MenuState.Main:

                    if (buttons["credits"].left)
                        menuState = MenuState.Credits;

                    if (buttons["options"].left)
                        menuState = MenuState.Options;

                    if (buttons["skill"].left)
                        callback("skill");

                    if (buttons["host"].left)
                        callback("startServer");

                    if (buttons["join"].left)
                        callback("joinServer");

                    break;

                case MenuState.Options:

                    if (buttons["back"].left)
                        menuState = MenuState.Main;

                    break;

                case MenuState.Credits:

                    if (buttons["back"].left)
                        menuState = MenuState.Main;

                    break;

            }
        }

        public void draw(Render2D view)
        {
            //Draws the buttons for each state
            foreach (String s in menuButtons[menuState])
                buttons[s].draw(view);
        }

    }
}
