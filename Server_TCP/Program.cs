
using DataAccess.Entities;
using System.Net;
using System.Net.Sockets;

namespace Server_TCP
{
    internal class Program
    {
        private const string localIp = "127.0.0.1";
        private const short localPort = 4000;

        static TcpListener server;
        static void Main(string[] args)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(localIp), localPort);
            server = new TcpListener(endPoint);
            server.Start();

            Chat chat = new Chat();

            while (true)
            {
                Console.WriteLine("Waiting for a request...");
                TcpClient client = server.AcceptTcpClient();

                Console.WriteLine($"\tGot a TcpClient!");

                ClientCommand req = GetClientRequest(client);

                Console.WriteLine($"\tGot a {req.Command} request!");

                if (req.Command == RequestCommand.Join)
                {
                    chat.AddMember(client);
                }
            }
        }
    }
}