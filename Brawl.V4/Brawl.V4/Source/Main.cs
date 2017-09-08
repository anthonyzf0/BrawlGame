using Brawl.V4.Source;
using Brawl.V4.Source.Game;
using Brawl.V4.Source.Menu.Gui;
using Brawl.V4.Source.Menu.Main;
using Brawl.V4.Source.Menu.Skills;
using Brawl.V4.Source.Server;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Brawl.V4
{
    public class Main : Game
    {
        //Window
        public static int windowX = 1200, windowY = 800;

        //Game state variable
        private enum GameState { Main, Skill, Play, StartServer, JoinServer };
        private static GameState gameState = GameState.Main;

        //Controllers for varios gamestates
        MainMenuController mainMenu;
        SkillMenuController skillMenu;
        ServerController server;
        GameController game;

        //Graphics conponents
        GraphicsDeviceManager graphics3D;     //3D renderer
        Render2D graphics2D;                  //2D renderer

        //inputs
        InputHandler inputs;

        //FPS
        Stopwatch timer;
        int frames = 0;
        float fps = 1;

        public Main()
        {
            graphics3D = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
       
        protected override void Initialize()
        {
            //Controllers
            mainMenu = new MainMenuController(adjustState);     //Main menu buttons
            skillMenu = new SkillMenuController(adjustState);   //Skill menu buttons
            inputs = new InputHandler();                        //Mouse clicking
            server = new ServerController();                    //Running server
            game = new GameController(graphics3D);              //Running client

            timer = new Stopwatch();
            timer.Start();

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
            //Frames
            frames++;
            if (timer.Elapsed.TotalSeconds > 1)
            {
                fps = frames;
                frames = 0;
                timer.Restart();
            }

            //TODO fix this
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Updates all the button values
            inputs.update();
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

                    game.update();

                    break;

                case GameState.StartServer:

                    //Starts server and plays
                    server.startServer();
                    gameState = GameState.JoinServer;

                    break;

                case GameState.JoinServer:

                    game.connect(Content);
                    gameState = GameState.Play;

                    break;
            }
            
            base.Update(gameTime);
        }

        public static int adjustState(string state)
        {
            if (state == "main")
                gameState = GameState.Main;
            if (state == "skill")
                gameState = GameState.Skill;
            if (state == "startServer")
                gameState = GameState.StartServer;
            if (state == "joinServer")
                gameState = GameState.JoinServer;
            
            return 0;
        }
        
        //Called to draw game
        protected override void Draw(GameTime gameTime)
        {
            //Clear display
            GraphicsDevice.Clear(Color.Black);
            
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

                    GraphicsDevice.DepthStencilState = DepthStencilState.Default;

                    game.draw();

                    break;
            }

            //Frames per sec
            graphics2D.start();

            graphics2D.drawText("FPS: " + fps, 0, 0);

            graphics2D.end();


            base.Draw(gameTime);
        }
    }
}
