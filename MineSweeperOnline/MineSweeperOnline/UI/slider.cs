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
    class Slider : Control
    {
        public Rectangle frontSlider;

        public int max;
        public int min;
        public string text;

        private Texture2D sliderFrontTexture;

        private SpriteFont font;

        private MouseState lastMouseState;

        public Slider(string text, int min, int max, Vector2 start,int width, int height)
        {
            this.text = text;
            this.min = min;
            this.max = max;
            rec = new Rectangle((int)start.X, (int)start.Y, width, height);
            //frontSlider = new Rectangle((int)start.X, (int)start.Y, (int)((float) width / (max - min) * 10), height);
            frontSlider = new Rectangle((int)start.X, (int)start.Y, 20, height);
        }

        public Slider(string text, int min, int max,Vector2 start, Vector2 end) : this(text,min,max,start,(int)(end.X - start.X),(int)(end.Y - start.Y))
        {

        }

        public Slider(string text, int min, int max, Vector2 start):this(text,min,max,start,config.SLIDER_WIDTH,config.SLIDER_HEIGHT)
        {
        }
        public override void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("fonts/buttonFont");

            texture = content.Load<Texture2D>("images/sliderBack");
            sliderFrontTexture = content.Load<Texture2D>("images/sliderFront");
        }

        public override void Update(GameTime gametime)
        {
            if(rec.Contains(Mouse.GetState().X, Mouse.GetState().Y) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                /*if(Mouse.GetState().X - lastMouseState.X > 0)
                {
                    if(frontSlider.X <= backSlider.X + backSlider.Width - frontSlider.Width)
                        frontSlider.Offset(Mouse.GetState().X - lastMouseState.X, 0);
                }
                else if(Mouse.GetState().X - lastMouseState.X < 0)
                {
                    if(frontSlider.X >= backSlider.X)
                        frontSlider.Offset(Mouse.GetState().X - lastMouseState.X, 0);
                }*/

                if(frontSlider.X + Mouse.GetState().X - lastMouseState.X >= rec.X && frontSlider.X + Mouse.GetState().X - lastMouseState.X <= rec.X + rec.Width - frontSlider.Width)
                    frontSlider.Offset(Mouse.GetState().X - lastMouseState.X, 0);
            }

            lastMouseState = Mouse.GetState();
        }

        public override void Draw(GameTime gametime, SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, rec, Color.White);
            spritebatch.Draw(sliderFrontTexture, frontSlider, Color.White);
            spritebatch.DrawString(font, text + ": " + getValue(), new Vector2(rec.X, rec.Y) + (new Vector2(rec.Width, rec.Height) / 2) - font.MeasureString(text + ": " + getValue()) / 2, Color.White);
        }

        public int getValue()
        {
            return (int)((float)(frontSlider.X - rec.X) / (rec.Width - frontSlider.Width) * (max - min) + min);
        }

        public void setValue(int value)
        {
            if(value >= min && value <= max)
            {
                frontSlider.X = (int)Math.Ceiling((float)(value - min) / (max - min) * (rec.Width - frontSlider.Width) + rec.X);
            }
        }

    }
}