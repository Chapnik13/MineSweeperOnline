using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MineSweeperOnline.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MineSweeperOnline.Screens
{
    class MultiPlayerStartScreen : Screen
    {
        private Button buttonJoin;
        private Button buttonHost;

        private Panel PanelOptions;
        public MultiPlayerStartScreen() : base("MultiPlayerStart")
        {
        }
        public override void Initialize()
        {
            buttonJoin = new Button("Join a server", new Vector2(0, 0));
            buttonHost = new Button("Host a server", new Vector2(300, 0));
            PanelOptions = new Panel(new Vector2(0, 0), buttonJoin, buttonHost);
            PanelOptions.setVec(new Vector2(config.WIDTH / 2 - PanelOptions.rec.Width / 2, config.HEIGHT / 2 - PanelOptions.rec.Height / 2));

            buttonHost.buttonClicked += buttonHost_buttonClicked;
            buttonJoin.buttonClicked += buttonJoin_buttonClicked;
            base.Initialize();
        }

        void buttonJoin_buttonClicked(object sender, EventArgs e)
        {
            Properties.Settings sett = new Properties.Settings();
            sett.IP = LocalIPAddress();
            sett.Save();
            config.refresh();
            config.host = false;
            changeScreen(screens["MultiPlayerScreen"]);
        }

        void buttonHost_buttonClicked(object sender, EventArgs e)
        {
            MSOServer.Server server = new MSOServer.Server();
            Process.Start(server.getPath());
            Properties.Settings sett = new Properties.Settings();
            sett.IP = LocalIPAddress();
            sett.Save();
            config.refresh();
            config.host = true;
            changeScreen(screens["MultiPlayerScreen"]);
        }

        public override void LoadContent(ContentManager content)
        {
            PanelOptions.LoadContent(content);
            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            PanelOptions.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spritebatch)
        {
            PanelOptions.Draw(gameTime, spritebatch);
            base.Draw(gameTime, spritebatch);
        }

        private string LocalIPAddress()
        {
            IPAddress localAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
            return localAddress.ToString();
        }
    }
}
