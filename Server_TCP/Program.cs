using System.Net;
using System.Net.Sockets;

namespace Server_TCP
{
    internal class Program
    {
        private const string localIp = "127.0.0.1";
        private const short localPort = 4000;
        static void Main(string[] args)
        {
            TcpListener server = new TcpListener(IPAddress.Parse(localIp), localPort);
            server.Start();
            Console.WriteLine("Server started and ready for requests...");
            while (true)
            {
                Console.WriteLine("Waiting for connection...");
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Connected!");
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[1024]; 
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                while ( bytesRead> 0)
                {
                    string data = System.Text.Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("Get " + data);

                    byte[] response = System.Text.Encoding.UTF8.GetBytes(data);
                    stream.Write(response, 0, response.Length);
                }
                stream.Close();
                client.Close();
                
            }
            server.Stop();
        }
    }
}