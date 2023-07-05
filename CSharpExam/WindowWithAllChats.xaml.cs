using DataAccess;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CSharpExam
{
   
    public partial class WindowWithAllChats : Window
    {
        User User { get; set; }
        public WindowWithAllChats()
        {
            InitializeComponent();
        }
        public WindowWithAllChats(User user)
        {
            this.User = user;
            InitializeComponent();
            
        }
        private void ConntectBTN_Click(object sender, RoutedEventArgs e)
        {
            if (chatWindowLstBox.SelectedItem != null)
            {
                var chat = chatWindowLstBox.SelectedItem as Chat;

                if (chat != null)
                {
                    MainWindow mainWindow = new MainWindow(User, chat);
                    mainWindow.Show();
                }
                else
                {
                    MessageBox.Show("Not found");
                }
            }
        }

        private void LeaveChatBTN_Click(object sender, RoutedEventArgs e)
        {

        }

        private void chatWindowLstBox_Loaded(object sender, RoutedEventArgs e)
        {
            using var dbContext = new MessengerDbContext();

            var chats = dbContext.UsersChats.Where(uch => uch.UserId == User.Id)
                                            .Select(uch => uch.Chat).ToList();

            foreach (var chat in chats)
                chatWindowLstBox.Items.Add(chat);
        }
    }
}