using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MineSweeperOnline.Screens
{
    class MultiPlayerScreen : Screen
    {
        private string ip;
        private int port;
        private TcpClient client;
        private NetworkStream ns;
        private MultiPlayerBoard board;

        Thread listenerThread;

        public MultiPlayerScreen() : base("MultiPlayerScreen")
        {
            
        }

        public override void Initialize()
        {
            listenerThread = new Thread(recieveBlock);
            ip = config.IP;
            port = 5353;
            Console.WriteLine(ip);
            client = new TcpClient();
            client.Connect(ip, port);
            ns = client.GetStream();

            if(config.host == true)
            {
                board = new MultiPlayerBoard(config.BOARD, config.BOARD, config.BOARD_BOMBS, new Vector2(config.WIDTH / 2 - (17 * config.BOARD) / 2, config.HEIGHT / 2 - (17 * config.BOARD) / 2));
                ns.Write(Encoding.ASCII.GetBytes(config.BOARD.ToString() + config.BOARD.ToString() + config.BOARD.ToString()), 0, Encoding.ASCII.GetBytes(config.BOARD.ToString() + config.BOARD.ToString() + config.BOARD.ToString()).Length);
                for(int i = 0; i < board.board.GetLength(0); i++)
                {
                    for(int j = 0; j < board.board.GetLength(1); j++)
                    {
                        string iSend = i.ToString(), jSend = j.ToString(), valueSend = board.board[i, j].value.ToString();
                        if(i >= 0 && i < 10)
                            iSend = "0" + i;

                        if(j >= 0 && j < 10)
                            jSend = "0" + j;

                        if(board.board[i, j].value >= 0 && board.board[i, j].value < 10)
                            valueSend = "0" + board.board[i, j].value;
                        ns.Write(Encoding.ASCII.GetBytes(iSend + jSend + valueSend), 0, Encoding.ASCII.GetBytes(iSend + jSend + valueSend).Length);
                    }
                }
                ns.Write(Encoding.ASCII.GetBytes("-1-1-1"), 0, Encoding.ASCII.GetBytes("-1-1-1").Length);
            }
            else
            {
                byte[] boardReceive = new byte[6];
                ns.Read(boardReceive, 0, boardReceive.Length);
                int[,] a = new int[int.Parse(Encoding.ASCII.GetString(boardReceive).Substring(0, 2)), int.Parse(Encoding.ASCII.GetString(boardReceive).Substring(0, 2))];
                string text;
                do
                {
                    ns.Read(boardReceive, 0, boardReceive.Length);
                    text = Encoding.ASCII.GetString(boardReceive);
                    if(text != "-1-1-1")
                        a[int.Parse(text.Substring(0, 2)), int.Parse(text.Substring(2, 2))] = int.Parse(text.Substring(4, 2));

                } while(text != "-1-1-1");
                board = new MultiPlayerBoard(a, new Vector2(config.WIDTH / 2 - (17 * config.BOARD) / 2, config.HEIGHT / 2 - (17 * config.BOARD) / 2));
            }

            board.blockClicked += board_blockClicked;
            listenerThread.Start();
            //listenerThread.Join();

            base.Initialize();
        }

        void board_blockClicked(object sender, BlockEventArgs e)
        {
            string iSend, jSend;
            if(e.i >= 0 && e.i <= 9)
                iSend = "0" + e.i.ToString();
            else
                iSend = e.i.ToString();

            if(e.j >= 0 && e.j <= 9)
                jSend = "0" + e.j.ToString();
            else
                jSend = e.j.ToString();

            ns.Write(Encoding.ASCII.GetBytes(iSend + jSend), 0, Encoding.ASCII.GetBytes(iSend + jSend).Length);
        }

        public override void LoadContent(ContentManager content)
        {
            board.LoadContent(content);
            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            board.Update(gameTime);
            

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spritebatch)
        {
            board.Draw(gameTime, spritebatch);
            base.Draw(gameTime, spritebatch);
        }

        private void recieveBlock()
        {
            byte[] bufferRecieved = new byte[4];
            ns.Read(bufferRecieved, 0, bufferRecieved.Length);
            Console.WriteLine(Encoding.ASCII.GetString(bufferRecieved));
            board.openBlocks(int.Parse(Encoding.ASCII.GetString(bufferRecieved).Substring(0, 2)), int.Parse(Encoding.ASCII.GetString(bufferRecieved).Substring(2, 2)));
        }
    }
}
