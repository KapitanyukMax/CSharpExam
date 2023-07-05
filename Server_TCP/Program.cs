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

        static MessengerDbContext dbContext = new MessengerDbContext();
        static Dictionary<TcpClient, User> connectedUsers = new Dictionary<TcpClient, User>();

        private static Message GetMessage(TcpClient client)
        {
            BinaryFormatter serializer = new BinaryFormatter();
            Message message = serializer.Deserialize(client.GetStream()) as Message;

            if (message is TextMessage textMessage)
                Console.WriteLine($"Received message: {textMessage.Text} : {message.SendingTime}");
            else if (message is FileMessage fileMessage)
                Console.WriteLine($"Received message: {fileMessage.Caption} : {message.SendingTime}");
            else
                Console.WriteLine($"Received command: {message.Command} : {message.SendingTime}");

            return message;
        }

        private static void SendResponse(TcpClient client, Message message)
        {
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(client.GetStream(), message);
        }

        private static void Listen(TcpClient client)
        {
            while (true)
            {
                Message message = GetMessage(client);

                if (message.Command == "MESSAGE")
                {
                    foreach (TcpClient connectedClient in connectedUsers.Keys)
                    {
                        SendResponse(connectedClient, message);
                        dbContext.Messages.Add(message);
                        dbContext.SaveChanges();
                    }
                }
                else if (message.Command == "LEAVE")
                {
                    connectedUsers.Remove(client);
                    client.Close();
                    break;
                }
            }
        }

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

            

            TcpListener server = new TcpListener(IPAddress.Parse(localIp), localPort);
            server.Start();
            Console.WriteLine("Server started and ready for requests...");

            while (true)
            {
                Console.WriteLine("Waiting for connection...");
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Tcp client connected");

                Message message = GetMessage(client);

                if (message.Command == "JOIN")
                {
                    if (connectedUsers.Keys.Contains(client))
                        continue;

                    if (!dbContext.Users.Contains(message.Sender))
                    {
                        dbContext.Users.Add(message.Sender);
                        dbContext.SaveChanges();
                    }

                    message.SenderId = dbContext.Users.FirstOrDefault(u =>
                            u.Login == message.Sender.Login &&
                            u.Password == message.Sender.Password)!.Id;
                    message.Sender.Id = message.SenderId;

                    connectedUsers.Add(client, message.Sender);

                    Listen(client);
                }
            }

            //while (true)
            //{
            //    BinaryFormatter serializer = new BinaryFormatter();

            //    using NetworkStream stream = client.GetStream();

            //    TextMessage message = serializer.Deserialize(stream) as TextMessage;

            //    if (message != null)
            //    {
            //        // Перевірка ідентифікації користувача
            //        bool isAuthenticated = true;//usersFromDB.Exists(u => u.Username == message.Sender);

            //        if (isAuthenticated)
            //        {
            //            // Збереження повідомлення у таблиці Message
            //            //dbContext.Messages.Add(message);
            //            //dbContext.SaveChanges();
            //        }
            //    }

            //    Console.WriteLine("Received message: " + message?.Text ?? "No message");

            //    //byte[] response = Encoding.UTF8.GetBytes(data);
            //    //stream.Write(response, 0, response.Length);
            //}
        }
    }
}