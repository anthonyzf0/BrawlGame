using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brawl.V4.Source.Menu.Gui
{
    class Render2D
    {
        //Used to draw 2d Images / text
        private SpriteBatch spriteBatch;

        //Font
        private SpriteFont font;

        //Textures
        private Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

        public Render2D(GraphicsDevice graphics, ContentManager content)
        {
            font = content.Load<SpriteFont>("Font");
            spriteBatch = new SpriteBatch(graphics);

            //All the textures to load
            List<String> textures = new List<string> { "button" };

            foreach (String s in textures)
                this.textures[s] = content.Load<Texture2D>(s);

        }

        //Starts / stops sprite batch
        public void start() { spriteBatch.Begin(); }
        public void end() { spriteBatch.End(); }
        
        //Draw specific things
        public void drawText(String text, Vector2 loc)
        {
            spriteBatch.DrawString(font, text, loc, Color.White);
        }
        public void drawText(String text, int x, int y)
        {
            drawText(text, new Vector2(x, y));
        }
        public void drawText(String text, Rectangle loc)
        {
            drawText(text, new Vector2(loc.Left, loc.Top));
        }
        public void drawTexture(String texture, Rectangle loc)
        {
            spriteBatch.Draw(textures[texture], loc, Color.White);
        }

    }
}
