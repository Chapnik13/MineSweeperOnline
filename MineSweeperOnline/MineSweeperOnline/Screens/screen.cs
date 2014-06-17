using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MineSweeperOnline.Screens
{
    class Screen
    {
        public static Dictionary<string, Screen> screens = new Dictionary<string, Screen>();
        public static Screen currentScreen;

        protected ContentManager content;
        //time till next screen in seconds -1 will not auto move
        protected float time;

        //next screen to show up after time is 0
        protected Screen nextScreen;

        //name of the screen
        protected string name;

        public static void init(ContentManager cm)
        {
            var targetAssembly = Assembly.GetExecutingAssembly();
            var subtypes = targetAssembly.GetTypes().Where(t => t.IsSubclassOf(typeof(Screen)));
            for(int i = 0; i < subtypes.ToArray().Length; i++)
            {
                subtypes.ToArray()[i].GetConstructors()[0].Invoke(new object[]{});
            }
            currentScreen = screens["Main Lobby"];
            currentScreen.Initialize();
            currentScreen.LoadContent(cm);
        }

        public Screen(string name)
        {
            time = -1;
            nextScreen = null;
            this.name = name;
            screens.Add(name, this);
        }

        public virtual void Initialize()
        {
            Console.WriteLine(name);
            setNextScreen();
        }

        public virtual void LoadContent(ContentManager cm)
        {
            content = new ContentManager(cm.ServiceProvider, cm.RootDirectory);
        }

        public virtual void UnLoadContent()
        {
            content.Unload();
        }

        public virtual void Update(GameTime gameTime)
        {
            if(time > -1)
            {
                float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                time -= elapsed;
                if(time <= 0)
                {
                    changeScreen(nextScreen);
                }
            }
            
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spritebatch)
        {

        }

        protected virtual void setNextScreen()
        {
        }

        protected string changeScreen(Screen next)
        {
            currentScreen.UnLoadContent();
            currentScreen = next;
            next.Initialize();
            next.LoadContent(content);
            return next.name;
        }
    }
}
