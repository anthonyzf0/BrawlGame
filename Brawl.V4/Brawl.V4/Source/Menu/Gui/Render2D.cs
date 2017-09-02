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
            List<String> textures = new List<string> { "button", "Node" };

            foreach (String s in textures)
                this.textures[s] = content.Load<Texture2D>(s);

            //Other textures
            this.textures["box"] = new Texture2D(graphics, 1, 1);
            this.textures["box"].SetData(new Color[] { new Color(Color.White,0.5f)});

        }

        //Starts / stops sprite batch
        public void start() { spriteBatch.Begin(); }
        public void end() { spriteBatch.End(); }

        //Draw Text
        public void drawText(String text, Vector2 loc)
        {
            spriteBatch.DrawString(font, text, loc, Color.White);
        }
        public void drawText(String text, Vector2 loc, Color c)
        {
            spriteBatch.DrawString(font, text, loc, c);
        }
        public void drawText(String text, int x, int y)
        {
            drawText(text, new Vector2(x, y));
        }
        public void drawText(String text, Rectangle loc)
        {
            drawText(text, new Vector2(loc.Left, loc.Top));
        }
        public void drawText(int centerx, int centery, String text, Color c)
        {
            drawText(text, new Vector2(centerx - text.Length*5, centery-10),c);
        }

        //Draw Texture
        public void drawTexture(String texture, Rectangle loc)
        {
            spriteBatch.Draw(textures[texture], loc, Color.White);
        }

        //Draw Sprite
        public void drawSprite(String texture, Vector2 pos, float size)
        {
            float scale = size / textures[texture].Width;
            Rectangle rect = textures[texture].Bounds;

            spriteBatch.Draw(textures[texture], pos, rect, Color.White, 0.0f, rect.Center.ToVector2(), scale, SpriteEffects.None, 0.0f);
        }

        //Draw Plain box
        public void drawBox(int x, int y, int w, int h, Color c)
        {
            spriteBatch.Draw(textures["box"], new Rectangle(x, y, w, h), c);
        }

    }
}
