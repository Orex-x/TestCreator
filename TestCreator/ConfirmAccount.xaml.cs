using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TestCreator.Objects;
using TestCreator.TestCreatorAPI;

namespace TestCreator
{
    /// <summary>
    /// Логика взаимодействия для ConfirmAccount.xaml
    /// </summary>
    public partial class ConfirmAccount : Window
    {
        private User user;
        public ConfirmAccount()
        {
            InitializeComponent();
        }
        public void setInfo(User user)
        {
            this.user = user;
            info.Content = "We sent you an email: " + user.email + "\n You can confirm registration by clicking on" +
               " the link in the letter, or insert the link here: ";
        }
        private void Button_Click_back(object sender, RoutedEventArgs e)
        {
            Client.deleteUserByLogin(user.login);
            SignUpWindow window = new SignUpWindow();
            window.setInfo(user);
            window.Show();
            Close();
        }

        private void Button_Click_confirmByTapLink(object sender, RoutedEventArgs e)
        {
            SignInWindow window = new SignInWindow();
            window.Show();
            Close();
        }

        private void Button_Click_ConfirmByLink(object sender, RoutedEventArgs e)
        {
            if (Client.confirmUser(link.Text))
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.init(user);
                mainWindow.Show();
                Database.savedata(
                    new User
                    {
                        login = user.login,
                        password = user.password
                    }
                );
                Close();
            }
        }
    }
}
