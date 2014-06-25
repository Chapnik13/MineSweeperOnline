using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MSOServer
{
    public class Server
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Your external IP address for sharing is "+getExternalIp());
            IPAddress IP = IPAddress.Parse(LocalIPAddress());
            int port = 5353;
            TcpListener listener = new TcpListener(IP, port);
            listener.Start();

            TcpClient player1 = listener.AcceptTcpClient();
            NetworkStream player1NS = player1.GetStream();
            while(!player1.Connected)
                ;

            Console.WriteLine("Player 1 Connected");

            TcpClient player2 = listener.AcceptTcpClient();
            NetworkStream player2NS = player2.GetStream();

            while(!player2.Connected)
                ;
            Console.WriteLine("Player 2 Connected");

            byte[] boardReceive = new byte[6];
            do
            {
                player1NS.Read(boardReceive, 0, boardReceive.Length);
                player2NS.Write(boardReceive, 0, boardReceive.Length);
            } while(Encoding.ASCII.GetString(boardReceive) != "-1-1-1");

            byte[] bufferReceive = new byte[4];
            string stringFromBytes;
            while(true)
            {
                player1NS.Read(bufferReceive, 0, bufferReceive.Length);
                stringFromBytes = Encoding.ASCII.GetString(bufferReceive);
                Console.WriteLine(stringFromBytes);
                player2NS.Write(bufferReceive, 0, bufferReceive.Length);

                player2NS.Read(bufferReceive, 0, bufferReceive.Length);
                stringFromBytes = Encoding.ASCII.GetString(bufferReceive);
                Console.WriteLine(stringFromBytes);
                player1NS.Write(bufferReceive, 0, bufferReceive.Length);
            }
        }

        public string getPath()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().Location;
        }

        private static string LocalIPAddress()
        {
            IPAddress localAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
            return localAddress.ToString();
        }

        private static string getExternalIp()
        {
            try
            {
                string externalIP;
                externalIP = (new WebClient()).DownloadString("http://checkip.dyndns.org/");
                externalIP = (new System.Text.RegularExpressions.Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}"))
                             .Matches(externalIP)[0].ToString();
                return externalIP;
            }
            catch
            {
                return null;
            }
        }
    }
}
