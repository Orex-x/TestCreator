using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
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



            if (File.Exists(Database.FILE_LOG))
            {
                User user = Database.getdata();
                login.Text = user.login;
                password.Password = user.password;
            }

        }

        private void Button_Click_signIn(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(password.Password) && !string.IsNullOrEmpty(login.Text))
            {
                User user = Client.getUserbyLogin(login.Text);
                if (user != null)
                {
                    if (user.password == password.Password)
                    {
                        if (!user.active)
                        {
                            ConfirmAccount confirmAccount = new ConfirmAccount();
                            confirmAccount.setInfo(user);
                            confirmAccount.Show();
                            Close();
                        }
                        else if (user.activationCode == null)
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
