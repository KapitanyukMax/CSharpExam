using DataAccess;
using DataAccess.Entities;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json.Nodes;

namespace Server_TCP
{
    internal class Program
    {
        private const string localIp = "127.0.0.1";
        private const short localPort = 4000;
        
        static void Main(string[] args)
        {
            //        HashSet<TcpClient> connectedClients = new HashSet<TcpClient>();
            //        List<User> usersFromDB = GetDataFromDatabase();
            //        TcpListener server = new TcpListener(IPAddress.Parse(localIp), localPort);
            //        server.Start();
            //        Console.WriteLine("Server started and ready for requests...");
            //        foreach (User user in usersFromDB)
            //        {
            //            //TcpClient client = new TcpClient();
            //            //client.Connect(localIp, localPort);
            //            TcpClient client = new TcpClient(user.IPAddress, localPort);
            //            connectedClients.Add(client);

            //        }
            //        while (true)
            //        {
            //            Console.WriteLine("Waiting for connection...");
            //            TcpClient client = server.AcceptTcpClient();
            //            Console.WriteLine("Connected!");
            //            NetworkStream stream = client.GetStream();
            //            byte[] buffer = new byte[1024];
            //            int bytesRead;
            //            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
            //            {
            //                string data = System.Text.Encoding.UTF8.GetString(buffer, 0, bytesRead);
            //                TextMessage message = JsonConvert.DeserializeObject<TextMessage>(data);
            //                if(message!=null)
            //                {
            //                    foreach (TcpClient connectedClient in connectedClients)
            //                    {
            //                        NetworkStream clientStream = connectedClient.GetStream();
            //                        byte[] messageBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            //                        clientStream.Write(messageBytes, 0, messageBytes.Length);
            //                    }
            //                }
            //                Console.WriteLine("Get " + data);

            //                byte[] response = System.Text.Encoding.UTF8.GetBytes(data);
            //                stream.Write(response, 0, response.Length);
            //            }
            //            stream.Close();
            //            client.Close();

            //        }
            //    }

            //    private static List<User> GetDataFromDatabase()
            //    {
            //        using (var context = new MessengerDbContext())
            //        {
            //            return context.Users.ToList();
            //        }
            //IPEndPoint localEbdPoint = new IPEndPoint(IPAddress.Parse(localIp), localPort);
            //server = new TcpListener(localEbdPoint);
            //server.Start();

            //Chat chat = new Chat();
            //while (true)
            //{
            //    Console.WriteLine("Waiting for request...");
            //    TcpClient tcpClient = server.AcceptTcpClient();

            //    Console.WriteLine("Got a TcpClient");

            //}
            HashSet<TcpClient> connectedClients = new HashSet<TcpClient>();
            using (var dbContext = new MessengerDbContext())
            {
                List<User> usersFromDB = dbContext.Users.ToList();
                TcpListener server = new TcpListener(IPAddress.Parse(localIp), localPort);
                server.Start();
                Console.WriteLine("Server started and ready for requests...");

                //foreach (User user in usersFromDB)
                //{
                //    TcpClient client = new TcpClient();
                //    client.Connect(localIp, localPort);
                //    connectedClients.Add(client);
                //}

                Console.WriteLine("Waiting for connection...");
                TcpClient client = server.AcceptTcpClient();
                //Console.WriteLine("Connected!");

                BinaryFormatter serializer = new BinaryFormatter();

                using NetworkStream stream = client.GetStream();

                TextMessage message = serializer.Deserialize(stream) as TextMessage;

                if (message != null)
                {
                    // Перевірка ідентифікації користувача
                    bool isAuthenticated = true;//usersFromDB.Exists(u => u.Username == message.Sender);

                    if (isAuthenticated)
                    {
                        // Збереження повідомлення у таблиці Message
                        //dbContext.Messages.Add(message);
                        //dbContext.SaveChanges();

                        // Розсилка повідомлення підключеним користувачам
                        //byte[] messageBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                        //foreach (TcpClient connectedClient in connectedClients)
                        //{
                        //    NetworkStream clientStream = connectedClient.GetStream();
                        //    clientStream.Write(messageBytes, 0, messageBytes.Length);
                        //}
                    }
                }

                Console.WriteLine("Received message: " + message?.Text ?? "No message");

                //byte[] response = Encoding.UTF8.GetBytes(data);
                //stream.Write(response, 0, response.Length);

                stream.Close();
                client.Close();
            }
        }
    }
}