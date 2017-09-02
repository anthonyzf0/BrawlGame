using Brawl.V4.Source.Menu.Gui;
using Brawl.V4.Source.Menu.Main;
using Brawl.V4.Source.Menu.Skills;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Brawl.V4
{
    public class Main : Game
    {
        //Window
        public static int windowX = 1200, windowY = 800;

        //Game state variable
        private enum GameState { Main, Skill, Play };
        private static GameState gameState = GameState.Main;

        //Controllers for varios gamestates
        MainMenuController mainMenu;
        SkillMenuController skillMenu;

        //Graphics conponents
        GraphicsDeviceManager graphics3D;     //3D renderer
        Render2D graphics2D;                  //2D renderer

        public Main()
        {
            graphics3D = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
       
        protected override void Initialize()
        {
            //Controllers
            mainMenu = new MainMenuController(adjustState);
            skillMenu = new SkillMenuController(adjustState);

            //Create spriteBatch for the graphics
            graphics2D = new Render2D(GraphicsDevice, Content);

            //Setup Window
            this.IsMouseVisible = true;
            graphics3D.PreferredBackBufferWidth = windowX;
            graphics3D.PreferredBackBufferHeight = windowY;
            graphics3D.ApplyChanges();

            base.Initialize();
        }
        
        protected override void Update(GameTime gameTime)
        {
            //TODO fix this
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Updates all the button values
            Button.update();

            switch (gameState)
            {
                //Update MainMenu
                case GameState.Main:

                    mainMenu.update();

                    break;

                case GameState.Skill:

                    skillMenu.update();

                    break;

                case GameState.Play:



                    break;
            }
            
            base.Update(gameTime);
        }

        public static int adjustState(string state)
        {
            if (state == "play")
                gameState = GameState.Play;
            if (state == "main")
                gameState = GameState.Main;
            if (state == "skill")
                gameState = GameState.Skill;

            //TODO FIX IDK why this need to be here, but the button callback wont work if this doesnt return a number
            return 0;
        }
        
        //Called to draw game
        protected override void Draw(GameTime gameTime)
        {
            //Clear display
            GraphicsDevice.Clear(Color.CornflowerBlue);

            switch (gameState)
            {
                //Main menu draw
                case GameState.Main:

                    graphics2D.start();
                    mainMenu.draw(graphics2D);
                    graphics2D.end();

                    break;

                case GameState.Skill:

                    graphics2D.start();
                    skillMenu.draw(graphics2D);
                    graphics2D.end();

                    break;

                case GameState.Play:
                    break;
            }

            base.Draw(gameTime);
        }
    }
}
