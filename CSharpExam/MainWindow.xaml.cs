using DataAccess.Entities;
using System;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

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

        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(User user)
        {
            InitializeComponent();

            this.user = user;
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
                Sender = user
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
                Sender = user
            });
            messageTxtBox.Clear();
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