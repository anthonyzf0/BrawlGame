using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brawl.V4.Source.Menu.Gui
{
    class Sprite
    {
        //Basic data
        private Vector2 pos;
        private String texture;

        //Other
        private float size = 40;
        private float targetSize;

        public Sprite(int x, int y, float s, string texture)
        {
            //Info
            pos = new Vector2(x, y);
            this.texture = texture;
            
            size = s;
            
        }

        public void scale(float size)
        {
            targetSize = size;
        }
        
        public void update()
        {
            size += (targetSize - size) / 4;
        }

        public void draw(Render2D view)
        {
            view.drawSprite(texture, pos, size);
        }

    }
}
