using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineSweeperOnline.UI
{
    class Button : Control
    {
        public string text;

        protected SpriteFont font;

        private bool on = false;

        protected MouseState lastMouseState;

        public event EventHandler buttonClicked;

        public Button(string text, Vector2 start, int width, int height)
        {
            this.text = text;
            this.rec = new Rectangle((int)start.X, (int)start.Y, width, height);
        }

        public Button(string text, Vector2 start, Vector2 end) : this(text,start,(int)(end.X - start.X),(int)(end.Y - start.Y))
        {
            
        }

        public Button(string text, Vector2 start) : this(text,start,config.BUTTON_WIDTH,config.BUTTON_HEIGHT)
        {
        }

        public override void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("fonts/buttonFont");

            texture = content.Load<Texture2D>("images/Button");
        }

        public override void Update(GameTime gametime)
        {
            if(rec.Contains(Mouse.GetState().X, Mouse.GetState().Y))
            {
                on = true;
                if(Mouse.GetState().LeftButton == ButtonState.Released && lastMouseState.LeftButton == ButtonState.Pressed && buttonClicked != null)
                {
                    buttonClicked(this, EventArgs.Empty);
                }
            }
            else
                on = false;

            if(rec.Width < font.MeasureString(text).X)
                rec.Width = (int)font.MeasureString(text).X;

            lastMouseState = Mouse.GetState();
        }

        public override void Draw(GameTime gametime, SpriteBatch spritebatch)
        {
            if(on == true)
            {
                Rectangle border = rec;
                border.Inflate(2,2);
                spritebatch.Draw(texture,border, Color.Black);
                //spritebatch.Draw(onButton, rec, Color.White);
            }
            spritebatch.Draw(texture, rec, Color.White);
            spritebatch.DrawString(font, text, new Vector2(rec.X, rec.Y) + (new Vector2(rec.Width,rec.Height) / 2) - font.MeasureString(text) / 2, Color.White);
        }
    }
}
