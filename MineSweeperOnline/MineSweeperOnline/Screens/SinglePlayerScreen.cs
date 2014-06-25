using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineSweeperOnline.Screens
{
    class SinglePlayerScreen : Screen
    {
        Board board;
        private int won;//0 not dont, -1 lost, 1 won
        private float time;

        private SpriteFont overFont, detailsFont;
        public SinglePlayerScreen() : base("SinglePlayer")
        {
            
        }

        public override void Initialize()
        {
            won = 0;
            time = 0;
            board = new Board(config.BOARD, config.BOARD, config.BOARD_BOMBS, new Vector2(config.WIDTH / 2 - (17 * config.BOARD) / 2, config.HEIGHT / 2 - (17 * config.BOARD) / 2));
            board.MineClicked += board_MineClicked;
            base.Initialize();
        }

        public override void LoadContent(ContentManager content)
        {
            overFont = content.Load<SpriteFont>("fonts/overFont");
            detailsFont = content.Load<SpriteFont>("fonts/blockFont");

            board.LoadContent(content);
            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.R))
                changeScreen(this);

            if(won == 0)
            {
                board.Update(gameTime);
                time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if(isOver() == true)
                    gameOver(true);
            }
            
            base.Update(gameTime);
        }

        private bool isOver()
        {
            int countClosed = 0;
            int countFlags = 0;
            for(int i = 0; i < board.board.GetLength(0); i++)
            {
                for(int j = 0; j < board.board.GetLength(1); j++)
                {
                    if(board.board[i, j].state == status.close)
                        countClosed++;
                    if(board.board[i, j].state == status.flag)
                        countFlags++;
                }
            }

            if(countClosed != 0 || countFlags != board.bombs)
                return false;
            return true;
        }
        
        void board_MineClicked(object sender, EventArgs e)
        {
            gameOver(false);
        }

        private void gameOver(bool win)
        {
            if(win == true)
            {
                Console.WriteLine("won");
                won = 1;
            }

            else
            {
                Console.WriteLine("Lost");
                won = -1;
            }
        }

        public override void Draw(GameTime gameTime,SpriteBatch spritebatch)
        {
            board.Draw(gameTime, spritebatch);
            int bombsLeft = board.bombs;
            for(int i = 0; i < board.board.GetLength(0); i++)
            {
                for(int j = 0; j < board.board.GetLength(1); j++)
                {
                    if(board.board[i, j].state == status.flag)
                        bombsLeft--;
                }
            }
            spritebatch.DrawString(detailsFont, "Bombs Left: " + bombsLeft, new Vector2(0, 30),Color.Black);
            spritebatch.DrawString(detailsFont, "Time passed: " + string.Format("{0:00}", time),new Vector2(0, 50), Color.Black);

            if(won != 0)
            {
                string endText;
                if(won == 1)
                    endText = "Game Ended, You have WON!";
                else
                    endText = "Game Ended, You have Lost...";
                Texture2D rect = new Texture2D(MineSweeperOnline.instance.GraphicsDevice,1,1);
                rect.SetData(new Color[]{Color.White});
                spritebatch.Draw(rect, new Rectangle((int)(config.WIDTH / 2 - overFont.MeasureString(endText).X / 2), (int)(config.HEIGHT / 2 - overFont.MeasureString(endText).Y / 2), (int)overFont.MeasureString(endText).X, (int)overFont.MeasureString(endText).Y), Color.White);
                spritebatch.DrawString(overFont, endText, new Vector2(config.WIDTH, config.HEIGHT) / 2 - overFont.MeasureString(endText) / 2, Color.Black);
            }
            base.Draw(gameTime,spritebatch);
        }
    }
}