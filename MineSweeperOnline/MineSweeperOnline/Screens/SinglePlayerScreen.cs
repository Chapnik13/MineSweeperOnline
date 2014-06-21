﻿using Microsoft.Xna.Framework;
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
        public SinglePlayerScreen() : base("SinglePlayer")
        {
            
        }

        public override void Initialize()
        {
            board = new Board(config.BOARD, config.BOARD, config.BOARD_BOMBS, new Vector2(config.WIDTH / 2 - (17 * config.BOARD) / 2, config.HEIGHT / 2 - (17 * config.BOARD) / 2));
            base.Initialize();
        }

        public override void LoadContent(ContentManager content)
        {
            board.LoadContent(content);
            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.R))
                changeScreen(this);
            board.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime,SpriteBatch spritebatch)
        {
            board.Draw(gameTime, spritebatch);
            base.Draw(gameTime,spritebatch);
        }
    }
}