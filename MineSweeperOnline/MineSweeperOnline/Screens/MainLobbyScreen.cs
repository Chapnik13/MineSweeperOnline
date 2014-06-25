using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MineSweeperOnline.UI;
using System.Diagnostics;
using System.Net.Sockets;

namespace MineSweeperOnline.Screens
{
    class MainLobbyScreen : Screen
    {
        private Button startSingle;
        private Button startMulti;
        private Button options;
        private Button exit;

        public MainLobbyScreen() : base("Main Lobby")
        {
            
        }

        public override void Initialize()
        {
            startSingle = new Button("Single", new Vector2((config.WIDTH - config.BUTTON_WIDTH) / 2, (config.HEIGHT - config.BUTTON_HEIGHT) * ((float)2 / 5)), config.BUTTON_WIDTH / 2, config.BUTTON_HEIGHT);
            startSingle.buttonClicked += onStartSingleClick;

            startMulti = new Button("Multi", new Vector2((config.WIDTH - config.BUTTON_WIDTH) / 2 + config.BUTTON_WIDTH / 2+1, (config.HEIGHT - config.BUTTON_HEIGHT) * ((float)2 / 5)), config.BUTTON_WIDTH / 2, config.BUTTON_HEIGHT);
            startMulti.buttonClicked += onStartMultiClick;

            options = new Button("Options", new Vector2((config.WIDTH - config.BUTTON_WIDTH) / 2, (config.HEIGHT - config.BUTTON_HEIGHT) * ((float)3 / 5)));
            options.buttonClicked += onOptionsClick;

            exit = new Button("Exit", new Vector2((config.WIDTH - config.BUTTON_WIDTH) / 2, (config.HEIGHT - config.BUTTON_HEIGHT) * ((float)4 / 5)));
            exit.buttonClicked += onExitClick;
            
            base.Initialize();
        }

        public override void LoadContent(ContentManager content)
        {
            startSingle.LoadContent(content);
            startMulti.LoadContent(content);
            options.LoadContent(content);
            exit.LoadContent(content);
            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            startSingle.Update(gameTime);
            startMulti.Update(gameTime);
            options.Update(gameTime);
            exit.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime,SpriteBatch spritebatch)
        {
            startSingle.Draw(gameTime, spritebatch);
            startMulti.Draw(gameTime, spritebatch);
            options.Draw(gameTime,spritebatch);
            exit.Draw(gameTime, spritebatch);
            base.Draw(gameTime,spritebatch);
        }

        private void onStartSingleClick(object sender, EventArgs e)
        {
            changeScreen(screens["SinglePlayer"]);
        }

        private void onStartMultiClick(object senrder, EventArgs e)
        {
            /*MSOServer.Server server = new MSOServer.Server();
            Process.Start(server.getPath());
            System.Threading.Thread.Sleep(1000);
            string IP = "192.168.1.12";
            int port = 5353;
            TcpClient client;
            NetworkStream ns;

            client = new TcpClient();
            client.Connect(IP, port);
            //while(true)
            {
                ns = client.GetStream();
                string bufferToSend = "CHECK";
                ns.Write(Encoding.ASCII.GetBytes(bufferToSend), 0, Encoding.ASCII.GetBytes(bufferToSend).Length);
            }*/
            changeScreen(screens["MultiPlayerStart"]);
        }

        private void onOptionsClick(object sender, EventArgs e)
        {
            changeScreen(screens["Options"]);
        }

        private void onExitClick(object sender, EventArgs e)
        {
            MineSweeperOnline.instance.Exit();
        }
    }
}
