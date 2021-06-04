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
    public partial class SignInWindow : Window
    {
        public SignInWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_signIn(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(password.Text) && !string.IsNullOrEmpty(login.Text))
            {
                User user = Client.getUserbyLogin(login.Text);
                if (user != null)
                {
                    if (user.password == password.Text)
                    {
                        if (user.activationCode == null)
                        {
                            MainWindow mainWindow = new MainWindow();
                            mainWindow.Show();
                            Close();
                        }
                        else
                            MessageBox.Show("Confirm your accout");
                    }
                    else
                    {
                        MessageBox.Show("wrong login or password");
                    }
                }
                else
                    MessageBox.Show("wrong login or password");
            }
        }

        private void Button_Click_signUp(object sender, RoutedEventArgs e)
        {
            SignUpWindow signUpWindow = new SignUpWindow();
            signUpWindow.Show();
            Close();
        }
    }
}
