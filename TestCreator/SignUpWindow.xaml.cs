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
    /// Логика взаимодействия для SignUpWindow.xaml
    /// </summary>
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
                if (password.Password.Length > 6 && password.Password == confirm_password.Password)
                {


                    User user = new User
                    {
                        first_name = first_name.Text,
                        surname = surname.Text,
                        last_name = last_name.Text,
                        login = login.Text,
                        password = password.Password,
                        email = "vipboy78038@gmail.com",
                        tests = new System.Collections.ObjectModel.ObservableCollection<Test>(),


                    };
                    if (Client.signUpUser(user))
                    {
                        visibilityAllElements(Visibility.Hidden);
                        ConfirmAccount confirmAccount = new ConfirmAccount();
                        confirmAccount.setInfo(user);
                        confirmAccount.Show();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Login is busy");
                    }
                  
                   
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
            lLogin.Visibility = visibility;
            password.Visibility = visibility;
            lPassword.Visibility = visibility;
            confirm_password.Visibility = visibility;
            lConfirm_password.Visibility = visibility;
            first_name.Visibility = visibility;
            lName.Visibility = visibility;
            surname.Visibility = visibility;
            lSurname.Visibility = visibility;
            last_name.Visibility = visibility;
            lLastname.Visibility = visibility;
            email.Visibility = visibility;
            lEmail.Visibility = visibility;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SignInWindow signInWindow = new SignInWindow();
            signInWindow.Show();
            this.Close();
        }
    }
}
