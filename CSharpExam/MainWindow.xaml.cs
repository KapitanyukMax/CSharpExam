using DataAccess;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;

namespace CSharpExam
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string serverIp = "127.0.0.1";
        private const int serverPort = 4000;

        private User user;
        private TcpClient client = new TcpClient();
        private Chat chat;

        public MainWindow(User user, Chat? selectedChat)
        {
            InitializeComponent();

            this.user = user;
            chat = selectedChat;
            ConnectToServer();
        }

        private async void ConnectToServer()
        {
            await client.ConnectAsync(IPAddress.Parse(serverIp), serverPort);

            SendMessage(new Message
            {
                SendingTime = DateTime.Now,
                Command = "JOIN",
                ChatId = 1,
                SenderId = user.Id
            });

            Listen();
        }

        private async void Listen()
        {
            while (true)
            {
                Message message = await GetMessageAsync();

                if (message.Command == "MESSAGE")
                {
                    if (message is TextMessage textMessage)
                        MessageBox.Show(textMessage.Text);
                    else if (message is FileMessage fileMessage)
                        MessageBox.Show(Path.GetFileName(fileMessage.Url) + "\n" + fileMessage.Caption);
                }
            }
        }

        public Task<Message> GetMessageAsync()
        {
            return Task.Run(() =>
            {
                BinaryFormatter serializer = new BinaryFormatter();
                return serializer.Deserialize(client.GetStream()) as Message ?? new Message();
            });
        }

        public void SendMessage(Message message)
        {
            NetworkStream stream = client.GetStream();
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(stream, message);
            stream.Flush();
        }

        private void addFileBTN_Click(object sender, RoutedEventArgs e)
        {

        }

        private void sendBTN_Click(object sender, RoutedEventArgs e)
        {
            SendMessage(new TextMessage
            {
                SendingTime = DateTime.Now,
                Command = "MESSAGE",
                Text = messageTxtBox.Text,
                ChatId = 1,
                SenderId = user.Id
            });
            messageTxtBox.Clear();
        }

        private void messageTxtBox_Loaded(object sender, RoutedEventArgs e)
        {
            using (var dbContext = new MessengerDbContext())
            {
                var chatWithMessages = dbContext.Chats.Include(c => c.Messages).FirstOrDefault(c => c.Id == chat.Id);
                if (chatWithMessages != null)
                {
                    foreach (var message in chatWithMessages.Messages)
                    {
                        mainTextBlock.Items.Add(message);
                    }
                }
            }
        }
        //private void sendBTN_Click(object sender, RoutedEventArgs e)
        //{
        //    string serverIp = "127.0.0.1";
        //    int serverPort = 4000;
        //    using (TcpClient client = new TcpClient())
        //    {
        //        client.Connect(serverIp, serverPort);
        //        NetworkStream stream = client.GetStream();
        //        TextMessage message = new TextMessage();
        //        message.Text = messageTxtBox.Text;
        //        byte[]messageBytes = Encoding.UTF8.GetBytes(message.Text);
        //        stream.Write(messageBytes, 0, messageBytes.Length);
        //        stream.Close();
        //        client.Close();
        //    }


        //}
    }
}