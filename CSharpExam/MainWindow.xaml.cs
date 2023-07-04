using DataAccess;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CSharpExam
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private User user;
        private Chat chat;
        public MainWindow(Chat? selectedChat)
        {
            InitializeComponent();
        }
        public MainWindow(User user)
        {
            this.user = user;

            InitializeComponent();

        }
        private void addFileBTN_Click(object sender, RoutedEventArgs e)
        {

        }
        private void sendBTN_Click(object sender, RoutedEventArgs e)
        {
            string serverIp = "127.0.0.1";
            int serverPort = 4000;

            using (TcpClient client = new TcpClient())
            {
                client.Connect(serverIp, serverPort);

                BinaryFormatter serializer = new BinaryFormatter();

                using (NetworkStream stream = client.GetStream())
                {
                    TextMessage message = new TextMessage
                    {
                        Text = messageTxtBox.Text,
                        ChatId = 1,
                        SenderId = user.Id
                    };
                    messageTxtBox.Clear();
                    
                    serializer.Serialize(stream, message);

                    stream.Flush();
                }

                client.Close();
            }
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
