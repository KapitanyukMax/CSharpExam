using DataAccess;
using DataAccess.Entities;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace Server_TCP
{
    internal class Program
    {
        private const string localIp = "127.0.0.1";
        private const short localPort = 4000;

        static MessengerDbContext dbContext = new MessengerDbContext();
        static Dictionary<TcpClient, User> connectedUsers = new Dictionary<TcpClient, User>();

        private static Task<Message?> GetMessageAsync(TcpClient client)
        {
            return Task.Run(() =>
            {
                Message message = new Message();
                try
                {
                    BinaryFormatter serializer = new BinaryFormatter();
                    message = serializer.Deserialize(client.GetStream()) as Message;
                }
                catch (Exception)
                {
                    connectedUsers.Remove(client);
                    client.Close();
                    return null;
                }

                if (message is TextMessage textMessage)
                    Console.WriteLine($"Received message: {textMessage.Text} : {message.SendingTime}");
                else if (message is FileMessage fileMessage)
                    Console.WriteLine($"Received message: {fileMessage.Caption} : {message.SendingTime}");
                else
                    Console.WriteLine($"Received command: {message.Command} : {message.SendingTime}");

                return message;
            });
        }

        private static void SendResponse(TcpClient client, Message message)
        {
            NetworkStream stream = client.GetStream();
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(stream, message);
            stream.Flush();
        }

        private static async void Listen(TcpClient client)
        {
            while (true)
            {
                Message? message = await GetMessageAsync(client);

                if (message == null)
                    break;

                if (message.Command == "MESSAGE")
                {
                    foreach (TcpClient connectedClient in connectedUsers.Keys)
                    {
                        dbContext.Messages.Add(message);
                        dbContext.SaveChanges();

                        message.Id = dbContext.Messages.FirstOrDefault(m => m == message).Id;

                        SendResponse(connectedClient, message);
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
            TcpListener server = new TcpListener(IPAddress.Parse(localIp), localPort);
            server.Start();
            Console.WriteLine("Server started and ready for requests...");
            Console.WriteLine("Waiting for connection...");

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Tcp client connected");

                BinaryFormatter serializer = new BinaryFormatter();
                Message message = serializer.Deserialize(client.GetStream()) as Message;

                if (message == null)
                    continue;
                else if (message is TextMessage textMessage)
                    Console.WriteLine($"Received message: {textMessage.Text} : {message.SendingTime}");
                else if (message is FileMessage fileMessage)
                    Console.WriteLine($"Received message: {fileMessage.Caption} : {message.SendingTime}");
                else
                    Console.WriteLine($"Received command: {message.Command} : {message.SendingTime}");

                if (message.Command == "JOIN")
                {
                    if (connectedUsers.Keys.Contains(client))
                        continue;

                    connectedUsers.Add(client, message.Sender);

                    Listen(client);
                }
            }
        }
    }
}