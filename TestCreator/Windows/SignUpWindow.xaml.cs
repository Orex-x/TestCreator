using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using TestCreator.Objects;
using TestCreator.TestCreatorAPI;

namespace TestCreator
{
    public partial class SignUpWindow : Window
    {
        public SignUpWindow()
        {
            InitializeComponent();
        }

        public void setInfo(User user)
        {
            first_name.Text = user.first_name;
            surname.Text = user.surname;
            last_name.Text = user.last_name;
            login.Text = user.login;
            email.Text = user.email;
            password.Password = user.password;
        }

        private void Button_ClickSignUp(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(first_name.Text) && !string.IsNullOrEmpty(surname.Text) &&
               !string.IsNullOrEmpty(last_name.Text) && !string.IsNullOrEmpty(login.Text) && !string.IsNullOrEmpty(email.Text)
            )
            {
                if (password.Password.Length > 6
                    && password.Password == confirm_password.Password)
                {
                    var user = new User
                    {
                        first_name = first_name.Text,
                        surname = surname.Text,
                        last_name = last_name.Text,
                        login = login.Text,
                        password = password.Password,
                        email = email.Text,
                        tests = new System.Collections.ObjectModel.ObservableCollection<Test>(),
                        active = true
                    };

                    if (Client.signUpUser(user))
                    {
                        visibilityAllElements(Visibility.Hidden);

                        var signInWindow = new SignInWindow();
                        signInWindow.Show();
                        this.Close();
                    }
                    else
                        MessageBox.Show("Login is busy");
                    
                  
                   
                }
                else
                {
                    MessageBox.Show("Passwords do not match or password is too small");
                }
            }
            else
            {
                MessageBox.Show("Fill in the fields");
            }
        }
        private void visibilityAllElements(Visibility visibility)
        {
            login.Visibility = visibility;
            password.Visibility = visibility;
            confirm_password.Visibility = visibility;
            first_name.Visibility = visibility;
            surname.Visibility = visibility;
            last_name.Visibility = visibility;
            email.Visibility = visibility;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var signInWindow = new SignInWindow();
            signInWindow.Show();
            this.Close();
        }
    }
}
