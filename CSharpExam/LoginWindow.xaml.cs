using DataAccess;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
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
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        string login;
        string pass;
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void loginBTN_Click(object sender, RoutedEventArgs e)
        {
            login = loginTxtBox.Text;
            pass = passwordTxtBox.Password;
            using (var dbContext = new MessengerDbContext())
            {
                var user = dbContext.Users.FirstOrDefault(c => c.Login == login && c.Password == pass);
                if (user != null)
                {
                    MainWindow mainWindow = new MainWindow(user);
                    mainWindow.Show();
                    this.Close();
                }
                else {
                    var result = MessageBox.Show("No such user. Dou you want to registrate?", "Incorrect input", MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        RegistrationWindow regwin = new RegistrationWindow();
                        regwin.Show();
                    }
                    else if (result == MessageBoxResult.No)
                    {
                        this.Close();
                    }
                }
            }
        }

        private void cancelBTN_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void registrationBTN_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registration = new RegistrationWindow();
            registration.Show();
        }
    }
}
