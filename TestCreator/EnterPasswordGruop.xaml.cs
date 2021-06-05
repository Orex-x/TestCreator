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
    /// Логика взаимодействия для EnterPasswordGruop.xaml
    /// </summary>
    public partial class EnterPasswordGruop : Window
    {

        private UserGroup userGroup;
        public EnterPasswordGruop()
        {
            InitializeComponent();
        }

        public void init(UserGroup userGroup)
        {
            this.userGroup = userGroup;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (userGroup.group.password == passwordBox.Password)
            {
                Boolean add = Convert.ToBoolean(Client.addUserGroup(userGroup));
                if (add)
                    MainWindow.groups.Add(userGroup);
            }
            else
                MessageBox.Show("Invalid password");
        }
    }
}
