#region Using Statements
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
        public static MineSweeperOnline instance;

        public MineSweeperOnline() : base()
        {
            instance = this;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            if(config.FULLSCREEN == true)
            {
                config.WIDTH = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                config.HEIGHT = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                graphics.IsFullScreen = true;
            }
            else
            {
                if(config.BOARD * 17 + 100 > config.WIDTH)
                    config.WIDTH = config.BOARD * 17 + 100;

                if(config.BOARD * 17 + 100 > config.HEIGHT)
                    config.HEIGHT = config.BOARD * 17 + 100;
                graphics.PreferredBackBufferWidth = config.WIDTH;
                graphics.PreferredBackBufferHeight = config.HEIGHT;
            }
            graphics.ApplyChanges();
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
