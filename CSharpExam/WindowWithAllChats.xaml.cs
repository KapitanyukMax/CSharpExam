using DataAccess;
using DataAccess.Entities;
using System.Linq;
using System.Windows;

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