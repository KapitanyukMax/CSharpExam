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
using System.Text.Json;

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

        private bool isListening = false;

        public MainWindow(User user, Chat selectedChat)
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
                ChatId = chat.Id,
                SenderId = user.Id
            });

            isListening = true;
            Listen();
        }

        private async void Listen()
        {
            while (isListening)
            {
                Message? message = await GetMessageAsync();

                if (message == null)
                    isListening = false;

                if (message?.Command == "MESSAGE")
                    mainListBox.Items.Add(message);
            }
        }

        public Task<Message?> GetMessageAsync()
        {
            return Task.Run(() =>
            {
                BinaryFormatter serializer = new BinaryFormatter();
                try
                {
                    return serializer.Deserialize(client.GetStream()) as Message;
                }
                catch (Exception)
                {
                    return null;
                }
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
                ChatId = chat.Id,
                SenderId = user.Id
            });
            messageTxtBox.Clear();
        }

        private void messageTxtBox_Loaded(object sender, RoutedEventArgs e)
        {
            using var dbContext = new MessengerDbContext();

            foreach (Message message in dbContext.Messages.Where(m => m.ChatId == chat.Id).ToList())
                mainListBox.Items.Add(message);
        }

        private void LeaveChatBTN_Click(object sender, RoutedEventArgs e)
        {
            SendMessage(new Message
            {
                SendingTime = DateTime.Now,
                Command = "LEAVE",
                SenderId = user.Id
            });

            isListening = false;

            Close();
        }
    }
}