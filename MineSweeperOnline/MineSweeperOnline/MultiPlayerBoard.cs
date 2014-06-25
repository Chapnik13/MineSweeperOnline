using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineSweeperOnline
{
    class MultiPlayerBoard
    {
        public int bombs;
        public Block[,] board;
        public Vector2 start;

        private Texture2D close, open, bomb;
        private SpriteFont BlockText;
        /*private bool ended;
        private bool won;*/

        //private SpriteFont overFont;
        private MouseState lastMouse;
        //private float time;

        public event EventHandler<BlockEventArgs> MineClicked;
        public event EventHandler<BlockEventArgs> blockClicked;

        public MultiPlayerBoard(int width, int height,int bombs, Vector2 start)
        {
            /*ended = false;
            time = 0;*/
            this.bombs = bombs;
            this.start = start;
            board = new Block[width, height];
            for(int i = 0; i < width;i++)
            {
                for(int j = 0; j < height;j++)
                {
                    board[i, j] = new Block(0, start + new Vector2(i * 17, j * 17), 16, 16);
                }
            }
                setBombs();
            createBoard();
        }

        public MultiPlayerBoard(int[,] a, Vector2 start)
        {
            bombs = 0;
            this.start = start;
            board = new Block[a.GetLength(0),a.GetLength(1)];
            for(int i = 0; i < board.GetLength(0);i++)
            {
                for(int j = 0; j < board.GetLength(1);j++)
                {
                    board[i, j] = new Block(a[i, j], start + new Vector2(i * 17, j * 17), 16, 16);
                    if(board[i, j].value == -1)
                        bombs++;
                }
            }
        }

        public void LoadContent(ContentManager content)
        {
            close = content.Load<Texture2D>("images/close");
            open = content.Load<Texture2D>("images/open");
            bomb = content.Load<Texture2D>("images/bomb");

            BlockText = content.Load<SpriteFont>("fonts/blockFont");
            //overFont = content.Load<SpriteFont>("fonts/overFont");
        }

        public void Update(GameTime gameTime)
        {
            /*if(ended == false)
            {

                float elapsed = (float)gametime.ElapsedGameTime.TotalSeconds;
                time += elapsed;
                if(isOver() == true)
                    gameOver(true);
            */
                if(Mouse.GetState().LeftButton == ButtonState.Released && lastMouse.LeftButton == ButtonState.Pressed)
                {
                    for(int i = 0; i < board.GetLength(0); i++)
                    {
                        for(int j = 0; j < board.GetLength(1); j++)
                        {
                            if(board[i, j].rec.Contains(Mouse.GetState().X, Mouse.GetState().Y) && board[i, j].state != status.flag && board[i, j].state != status.open)
                            {
                                if(blockClicked != null)
                                    blockClicked(board[i, j], new BlockEventArgs(i, j));
                                openBlocks(i, j);
                            }
                        }
                    }
                }


                if(Mouse.GetState().RightButton == ButtonState.Released && lastMouse.RightButton == ButtonState.Pressed)
                {
                    for(int i = 0; i < board.GetLength(0); i++)
                    {
                        for(int j = 0; j < board.GetLength(1); j++)
                        {
                            if(board[i, j].rec.Contains(Mouse.GetState().X, Mouse.GetState().Y) && board[i, j].state != status.open)
                            {
                                if(board[i, j].state == status.close)
                                    board[i, j].state = status.flag;
                                else if(board[i, j].state == status.flag)
                                    board[i, j].state = status.close;
                            }
                        }
                    }
                }

                lastMouse = Mouse.GetState();
            //}
        }

        public void openBlocks(int i, int j)
        {
            if(i >= 0 && board.GetLength(0) > i && j >= 0 && board.GetLength(1) > j && board[i, j].state == status.close)
            {
                board[i, j].state = status.open;
                if(board[i, j].value == -1 && MineClicked != null)
                {
                    MineClicked(board[i, j], new BlockEventArgs(i, j));
                }
                else
                if(board[i, j].value == 0)
                {
                    openBlocks(i - 1, j - 1);
                    openBlocks(i - 1, j);
                    openBlocks(i - 1, j + 1);
                    openBlocks(i, j - 1);
                    openBlocks(i, j + 1);
                    openBlocks(i + 1, j - 1);
                    openBlocks(i + 1, j);
                    openBlocks(i + 1, j + 1);
                }
            }
        }

        /*private bool isOver()
        {
            int countClosed = 0;
            int countFlags = 0;
            for(int i = 0; i < board.GetLength(0);i++)
            {
                for(int j = 0; j < board.GetLength(1);j++)
                {
                    if(board[i, j].state == status.close)
                        countClosed++;
                    if(board[i, j].state == status.flag)
                        countFlags++;
                }
            }

            if(countClosed != 0 || countFlags != bombs)
                return false;
            return true;
        }

        private void gameOver(bool win)
        {
            if(win == true)
            {
                Console.WriteLine("won");
                won = true;
            }

            else
            {
                Console.WriteLine("Lost");
                won = false;
            }
            ended = true;
        }*/

        public void Draw(GameTime gametime,SpriteBatch spritebatch)
        {
            /*int bombsLeft = bombs;
            for(int i = 0; i < board.GetLength(0); i++)
            {
                for(int j = 0; j < board.GetLength(1); j++)
                {
                    if(board[i, j].state == status.flag)
                        bombsLeft--;
                }
            }
            spritebatch.DrawString(BlockText, "Bombs Left: " + bombsLeft, start - new Vector2(0, 30),Color.Black);
            spritebatch.DrawString(BlockText, "Time passed: " + string.Format("{0:00}",time), start - new Vector2(0, 50), Color.Black);*/
            for(int i = 0; i < board.GetLength(0);i++)
            {
                for(int j = 0; j < board.GetLength(1);j++)
                {
                    board[i, j].Draw(gametime, spritebatch, close, open, bomb, BlockText, Color.Red);
                }
            }
            /*if(ended == true)
            {
                string endText;
                if(won == true)
                    endText = "Game Ended, You have WON!";
                else
                    endText = "Game Ended, You have Lost...";
                spritebatch.Draw(open, new Rectangle((int)(config.WIDTH / 2 - overFont.MeasureString(endText).X / 2), (int)(config.HEIGHT / 2 - overFont.MeasureString(endText).Y / 2), (int)overFont.MeasureString(endText).X, (int)overFont.MeasureString(endText).Y), Color.White);
                spritebatch.DrawString(overFont, endText, new Vector2(config.WIDTH, config.HEIGHT) / 2 - overFont.MeasureString(endText) / 2, Color.Black);
            }*/
        }

        private void setBombs()
        {
            Random rnd = new Random();
            int num;
            for(int i = 0; i < bombs;i++)
            {
                num = rnd.Next(board.GetLength(0) * board.GetLength(1));
                if(board[num / board.GetLength(0), num % board.GetLength(1)].value != -1)
                    board[num / board.GetLength(0), num % board.GetLength(1)].value = -1;
                else
                    i--;

            }
        }

        private void createBoard()
        {
            for(int i = 0; i < board.GetLength(0);i++)
            {
                for(int j = 0; j < board.GetLength(1); j++)
                {
                    if(board[i, j].value != -1)
                    {
                        if(i - 1 >= 0 && board.GetLength(0) > i - 1)
                        {
                            if(j - 1 >= 0 && board.GetLength(1) > j - 1)
                            {
                                if(board[i - 1, j - 1].value == -1)
                                    board[i, j].value++;
                            }

                            if(j >= 0 && board.GetLength(1) > j)
                            {
                                if(board[i - 1, j].value == -1)
                                    board[i, j].value++;
                            }

                            if(j + 1 >= 0 && board.GetLength(1) > j + 1)
                            {
                                if(board[i - 1, j + 1].value == -1)
                                    board[i, j].value++;
                            }
                        }

                        if(i >= 0 && board.GetLength(0) > i)
                        {
                            if(j - 1 >= 0 && board.GetLength(1) > j - 1)
                            {
                                if(board[i, j - 1].value == -1)
                                    board[i, j].value++;
                            }

                            if(j + 1 >= 0 && board.GetLength(1) > j + 1)
                            {
                                if(board[i, j + 1].value == -1)
                                    board[i, j].value++;
                            }
                        }

                        if(i + 1 >= 0 && board.GetLength(0) > i + 1)
                        {
                            if(j - 1 >= 0 && board.GetLength(1) > j - 1)
                            {
                                if(board[i + 1, j - 1].value == -1)
                                    board[i, j].value++;
                            }

                            if(j >= 0 && board.GetLength(1) > j)
                            {
                                if(board[i + 1, j].value == -1)
                                    board[i, j].value++;
                            }

                            if(j + 1 >= 0 && board.GetLength(1) > j + 1)
                            {
                                if(board[i + 1, j + 1].value == -1)
                                    board[i, j].value++;
                            }
                        }
                    }
                }
            }
        }
    }
}
