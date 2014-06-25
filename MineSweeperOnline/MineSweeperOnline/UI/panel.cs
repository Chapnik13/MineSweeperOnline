using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineSweeperOnline.UI
{
    class Panel : Control
    {
        public List<Control> controls;
        public Panel(Vector2 start, int width, int height,params Control[] control)
        {
            rec = new Rectangle((int)start.X, (int)start.Y, width, height);
            controls = new List<Control>();
            if(control != null)
                for(int i = 0; i < control.Length;i++)
                {
                    addControl(control[i]);
                }
        }

        public Panel(Vector2 vec,params Control[] control) : this(vec, 0, 0,control)
        {

        }

        public Panel(Vector2 vec) : this(vec,0,0,null)
        {

        }

        public Panel(Vector2 start, Vector2 end) : this(start,(int)(end.X - start.X),(int)(end.Y - start.Y))
        {
            
        }

        public void addControl(Control control)
        {
            Rectangle bx = Rectangle.Empty, by = Rectangle.Empty;
            control.rec.X += rec.X;
            control.rec.Y += rec.Y;
            controls.Add(control);
            foreach(Control ctl in controls)
            {
                if(ctl.rec.X + ctl.rec.Width > bx.X + bx.Width)
                    bx = ctl.rec;

                if(ctl.rec.Y + ctl.rec.Height > by.Y + bx.Height)
                    by = ctl.rec;
            }

            rec.Width = bx.X + bx.Width - rec.X;
            rec.Height = by.Y + by.Height - rec.Y;
        }

        public override void LoadContent(ContentManager content)
        {
            foreach(Control control in controls)
            {
                control.LoadContent(content);
            }
        }

        public void setVec(Vector2 vec)
        {
            foreach(Control control in controls)
            {
                control.rec.X -= rec.X;
                control.rec.Y -= rec.Y;
            }
            rec.X = (int)vec.X;
            rec.Y = (int)vec.Y;
            foreach(Control control in controls)
            {
                control.rec.X += rec.X;
                control.rec.Y += rec.Y;
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach(Control control in controls)
            {
                control.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spritebatch)
        {
            foreach(Control control in controls)
            {
                //spritebatch.Draw(control.texture, new Rectangle(control.rec.X + rec.X, control.rec.Y + rec.Y, control.rec.Width, control.rec.Height), Color.White);
                control.Draw(gameTime, spritebatch);
            }
        }

    }
}
