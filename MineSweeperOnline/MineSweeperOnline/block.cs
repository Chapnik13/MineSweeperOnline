using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineSweeperOnline
{
    enum status
    {
        open,
        close,
        flag
    }
    class Block
    {
        public status state;
        public int value;
        public Rectangle rec;

        public Block(int value, Vector2 start, int width, int height)
        {
            rec = new Rectangle((int)start.X, (int)start.Y, width, height);
            this.value = value;
            state = status.close;
        }

        public Block(int value, Vector2 start, Vector2 end) : this(value,start,(int)(end.X-start.X),(int)(end.Y-start.Y))
        {
        }

        public void Draw(GameTime gametime, SpriteBatch spritebatch, Texture2D close, Texture2D open, Texture2D bomb, SpriteFont BlockText, Color flagColor)
        {
            switch(state)
            {
                case status.close:
                    spritebatch.Draw(close, rec, Color.White);
                    break;

                case status.flag:
                    spritebatch.Draw(close, rec, Color.White);
                    spritebatch.DrawString(BlockText, "F", new Vector2(rec.X, rec.Y) + (new Vector2(rec.Width, rec.Height) / 2) - BlockText.MeasureString("F") / 2, flagColor);
                    break;

                case status.open:
                    if(value == 0)
                        spritebatch.Draw(open, rec, Color.White);
                    else if(value == -1)
                        spritebatch.Draw(bomb, rec, Color.White);
                    else
                    {
                        spritebatch.Draw(open, rec, Color.White);
                        switch(value)
                        {
                            case 1:
                                spritebatch.DrawString(BlockText, value.ToString(), new Vector2(rec.X, rec.Y) + (new Vector2(rec.Width, rec.Height) / 2) - (BlockText.MeasureString(value.ToString()) / 2), Color.Red);
                                break;
                            case 2:
                                spritebatch.DrawString(BlockText, value.ToString(), new Vector2(rec.X, rec.Y) + (new Vector2(rec.Width, rec.Height) / 2) - (BlockText.MeasureString(value.ToString()) / 2), Color.Blue);
                                break;
                            case 3:
                                spritebatch.DrawString(BlockText, value.ToString(), new Vector2(rec.X, rec.Y) + (new Vector2(rec.Width, rec.Height) / 2) - (BlockText.MeasureString(value.ToString()) / 2), Color.Green);
                                break;
                            case 4:
                                spritebatch.DrawString(BlockText, value.ToString(), new Vector2(rec.X, rec.Y) + (new Vector2(rec.Width, rec.Height) / 2) - (BlockText.MeasureString(value.ToString()) / 2), Color.Cyan);
                                break;
                            case 5:
                                spritebatch.DrawString(BlockText, value.ToString(), new Vector2(rec.X, rec.Y) + (new Vector2(rec.Width, rec.Height) / 2) - (BlockText.MeasureString(value.ToString()) / 2), Color.Tan);
                                break;
                            default:
                                spritebatch.DrawString(BlockText, value.ToString(), new Vector2(rec.X, rec.Y) + (new Vector2(rec.Width, rec.Height) / 2) - (BlockText.MeasureString(value.ToString()) / 2), Color.Black);
                                break;
                        }
                    }
                    break;
            }
        }
    }
}
