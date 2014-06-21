using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineSweeperOnline.UI
{
    abstract class Control
    {
        public Rectangle rec;
        public Texture2D texture;

        public abstract void LoadContent(ContentManager content);
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gametime, SpriteBatch spritebatch);
    }
}
