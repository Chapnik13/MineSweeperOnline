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

        Slider slider;

        public OptionsScreen() : base("Options")
        {
            slider = new Slider("Check", 0, 300, new Vector2(300, 300));
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void LoadContent(ContentManager content)
        {
            slider.LoadContent(content);
            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime, Game game)
        {
            slider.Update(gameTime);
            base.Update(gameTime,game);
        }

        public override void Draw(GameTime gameTime,SpriteBatch spritebatch)
        {
            slider.Draw(gameTime, spritebatch);
            base.Draw(gameTime,spritebatch);
        }
    }
}
