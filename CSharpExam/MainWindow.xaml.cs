using DataAccess;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
        
        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(User user)
        {
            string login = user.Login;
            string password = user.Password;

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
                NetworkStream stream = client.GetStream();
                TextMessage message = new TextMessage();
                message.Text = messageTxtBox.Text;
                byte[]messageBytes = Encoding.UTF8.GetBytes(message.Text);
                stream.Write(messageBytes, 0, messageBytes.Length);
                stream.Close();
                client.Close();
            }
            

        }
    }
}
