using System.IO;
using System.Windows;
using TestCreator.Objects;
using TestCreator.Service;
using TestCreator.TestCreatorAPI;
using TestCreator.Windows;

namespace TestCreator
{
    public partial class SignInWindow : Window
    {
        public SignInWindow()
        {
            InitializeComponent();

            if (File.Exists(Database.FILE_LOG))
            {
                var user = Database.getdata();
                login.Text = user.login;
                password.Password = user.password;
            }
        }

        private void Button_Click_signIn(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(password.Password) && !string.IsNullOrEmpty(login.Text))
            {
                var user = Client.getUserbyLogin(login.Text);
                if (user != null)
                {
                    if (user.password.Equals(password.Password))
                    {
                        StaticObject.mainUser = user;
                        var mainWindow = new MainWindow();

                        mainWindow.Show();

                        Database.savedata(
                            new User
                            {
                                login = user.login,
                                password = user.password
                            }
                            );

                        HomeWindow w = new HomeWindow();
                        w.Show();
                        Close();
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
            var signUpWindow = new SignUpWindow();
            signUpWindow.Show();
            Close();
        }
    }
}
