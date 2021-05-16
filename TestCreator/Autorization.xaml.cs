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

namespace TestCreator
{
    /// <summary>
    /// Логика взаимодействия для Autorization.xaml
    /// </summary>
    public partial class Autorization : Window
    {
        public Autorization()
        {
            InitializeComponent();
        }

        private void Button_Click_Registr(object sender, RoutedEventArgs e)
        {
            
            if(TextBoxConfirmPassword.Visibility == Visibility.Hidden)
            {
                TextBoxConfirmPassword.Visibility = Visibility.Visible;
                LabelConfirmPasssword.Visibility = Visibility.Visible;
            }
            else
            {
                if(TextBoxPassword.Password.Length > 3)
                {
                    if (TextBoxConfirmPassword.Password == TextBoxPassword.Password)
                    {
                        //Добавить аккаунт в бд
                        MainWindow window = new MainWindow();
                        window.Show();
                        this.Close();
                    }
                    else
                        MessageBox.Show("Пароли не совпадают");
                }
                else
                    MessageBox.Show("Пароль слишком короткий");

            }
        }
        private void Button_Click_Signin(object sender, RoutedEventArgs e)
        {

        }

    }
}
