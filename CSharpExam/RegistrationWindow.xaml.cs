using DataAccess;
using DataAccess.Entities;
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
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        Credential credential;
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void confirmBTN_Click(object sender, RoutedEventArgs e)
        {
            
           
            string password = passwordTXTBX.Password;
            string confPass = confirmPassTXTBX.Password;
         
            if (password!=confPass)
            {
                MessageBox.Show("You entered different passwords! Try one more time");
                passwordTXTBX.Clear();
                confirmPassTXTBX.Clear();
            }
            else
            {
                using(var context = new MessengerDbContext())
            {
                    var newUser = new Credentials
                    {
                        Login = loginTXTBX.Text,
                        Password = passwordTXTBX.Password,
                        Name = nameTXTBX.Text,
                        PhoneNumber = phoneTXTBX.Text,
                        MailAddress = emailTXTBX.Text
                    };
                    context.Credentials.Add(newUser);
                    context.SaveChanges();
                    MessageBox.Show("Successfuly registration");

                    this.Close();
                }
            }
        }

        private void clearBTN_Click(object sender, RoutedEventArgs e)
        {
            var textBoxes = RegistrationWindow.FindTextBoxes(this);
            foreach (var textBox in textBoxes)
            {
                textBox.Text = string.Empty;
            }
        }

        private void cancelBTN_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public static IEnumerable<TextBox> FindTextBoxes(Window window)
        {
            var textboxes = new List<TextBox>();

            foreach (var child in LogicalTreeHelper.GetChildren(window))
            {
                if (child is TextBox textBox)
                {
                    textboxes.Add(textBox);
                }
                else if (child is DependencyObject dependencyObject)
                {
                    textboxes.AddRange(FindTextBoxes(dependencyObject));
                }
            }

            return textboxes;
        }

        private static IEnumerable<TextBox> FindTextBoxes(DependencyObject parent)
        {
            var textboxes = new List<TextBox>();

            int childCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is TextBox textBox)
                {
                    textboxes.Add(textBox);
                }

                textboxes.AddRange(FindTextBoxes(child));
            }

            return textboxes;
        }
    }
}
