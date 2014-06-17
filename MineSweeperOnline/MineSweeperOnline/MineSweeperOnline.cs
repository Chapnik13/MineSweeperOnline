﻿#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using MineSweeperOnline.Screens;
#endregion

namespace MineSweeperOnline
{
    public class MineSweeperOnline : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public MineSweeperOnline() : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            if(config.BOARD_WIDTH * 17 + 100 > config.WIDTH)
                config.WIDTH = config.BOARD_WIDTH * 17 + 100;

            if(config.BOARD_HEIGHT * 17 + 100 > config.HEIGHT)
                config.HEIGHT = config.BOARD_HEIGHT * 17 + 100;
            graphics.PreferredBackBufferWidth = config.WIDTH;
            graphics.PreferredBackBufferHeight = config.HEIGHT;
        }

        protected override void Initialize()
        {
            Screen.init(Content);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Screen.currentScreen.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            Screen.currentScreen.Draw(gameTime,spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}