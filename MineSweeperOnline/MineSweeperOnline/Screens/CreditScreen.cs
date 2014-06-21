using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineSweeperOnline.Screens
{
    class CreditScreen : Screen
    {
        SpriteFont font;
        public CreditScreen() : base("Credits")
        {
            
        }

        protected override void setNextScreen()
        {
            time = 2;
            nextScreen = screens["Game Name"];
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("fonts/CreditsFont");
            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime,SpriteBatch spritebatch)
        {
            spritebatch.DrawString(font, "Made by Chapnik", (new Vector2(config.WIDTH,config.HEIGHT) - font.MeasureString("Made by Chapnik")) / 2, Color.Black);
            base.Draw(gameTime,spritebatch);
        }
    }
}