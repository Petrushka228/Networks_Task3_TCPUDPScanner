using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace UDPTCPScaner
{
    static class Program
    {
        private static int from = 0;
        private static int to = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("Сканер запущен...\nВведите диапазон портов(через пробел)");
            var input = Console.ReadLine();
            from = int.Parse(input.Split(" ")[0]);
            to = int.Parse(input.Split(" ")[1]);
            Console.WriteLine("Сканирование запущено...");
            for (int i = from; i <= to; i++)
            {
                CheckIfPortIsOpen(i);
            }
            Console.WriteLine("Сканирование завершено...");

        }

        public static void CheckIfPortIsOpen(int port)
        {
            Thread thread = new Thread(new ThreadStart(() =>
            {
                try
                {
                    var tcpClient = new TcpClient();
                    tcpClient.Connect("127.0.0.1", port);
                    Console.WriteLine("TCP порт " + port + " открыт");
                }
                catch (SocketException)
                {
                    
                }

                try
                {
                    UdpClient udpClient = new UdpClient(port);
                    Console.WriteLine("UDP порт " + port + " открыт");
                    
                }
                catch (SocketException ex)
                {
                    
                }
            }));
            thread.Start();
        }
    }
}
