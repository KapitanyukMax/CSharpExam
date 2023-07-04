using DataAccess.Entities;
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

        }

        private void LeaveChatBTN_Click(object sender, RoutedEventArgs e)
        {

        }

        private void chatWindowLstBox_Loaded(object sender, RoutedEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            foreach (var chat in User.UsersChats)
            {
                listBox.Items.Add(chat.Chat.Name);
            }
        }
    }
}
