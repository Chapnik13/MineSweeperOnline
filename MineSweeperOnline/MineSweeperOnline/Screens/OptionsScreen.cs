using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MineSweeperOnline.UI;

namespace MineSweeperOnline.Screens
{
    class OptionsScreen : Screen
    {

        Slider boardSize, bombs;
        Button accept, close;
        SpriteFont font;
        int time = 0;
        public OptionsScreen() : base("Options")
        {
        }

        public override void Initialize()
        {
            boardSize = new Slider("Board Size", 12, 50, new Vector2(50, 50));
            boardSize.setValue(config.BOARD);
            bombs = new Slider("Bombs", 12, 100, new Vector2(400, 50));
            bombs.setValue(config.BOARD_BOMBS);

            accept = new Button("Save & Close", new Vector2(150, 400));
            accept.buttonClicked += accept_buttonClicked;

            close = new Button("Close Without Saving", new Vector2(400, 400));
            close.buttonClicked += close_buttonClicked;
            base.Initialize();
        }

        void close_buttonClicked(object sender, EventArgs e)
        {
            changeScreen(screens["Main Lobby"]);
        }

        void accept_buttonClicked(object sender, EventArgs e)
        {
            Properties.Settings sett = new Properties.Settings();
            sett.Bombs = (byte)bombs.getValue();
            sett.Size = (byte)boardSize.getValue();
            sett.Save();
            config.refresh();
            changeScreen(screens["Main Lobby"]);
        }

        public override void LoadContent(ContentManager content)
        {
            boardSize.LoadContent(content);
            bombs.LoadContent(content);

            accept.LoadContent(content);
            close.LoadContent(content);

            font = content.Load<SpriteFont>("fonts/buttonFont");


            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime, Game game)
        {
            boardSize.Update(gameTime);
            bombs.Update(gameTime);
            accept.Update(gameTime);
            close.Update(gameTime);

            bombs.min = boardSize.getValue();
            base.Update(gameTime,game);
        }

        public override void Draw(GameTime gameTime,SpriteBatch spritebatch)
        {
            //board size
            spritebatch.DrawString(font, "Board Size", new Vector2(50, 20), Color.White);
            boardSize.Draw(gameTime, spritebatch);

            //bombs
            spritebatch.DrawString(font, "Amount of Bombs", new Vector2(400, 20), Color.White);
            bombs.Draw(gameTime, spritebatch);

            //buttons
            accept.Draw(gameTime, spritebatch);
            close.Draw(gameTime, spritebatch);

            base.Draw(gameTime,spritebatch);
        }
    }
}
