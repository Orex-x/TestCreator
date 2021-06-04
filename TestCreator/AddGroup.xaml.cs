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
    /// Логика взаимодействия для AddGroup.xaml
    /// </summary>
    public partial class AddGroup : Window
    {
        public AddGroup()
        {
            InitializeComponent();
        }

        private void Button_Click_AddGroup(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(group_name.Text) && !string.IsNullOrEmpty(group_password.Text))
            {
                Group Addgroup = new Group
                {
                    name = group_name.Text,
                    password = group_password.Text
                };
                if (radioButtonPublic.IsChecked == true)
                    Addgroup.security_status = Group.Security.Public;
                else
                    Addgroup.security_status = Group.Security.Private;

                long id = Convert.ToInt32(Client.addGroup(Addgroup));
                Addgroup.id_group = id;

                UserGroup userGroup = new UserGroup()
                {
                    is_admin = true,
                    group = Addgroup,
                    user = MainWindow.mainUser
                };

                Client.addUserGroup(userGroup);
                MainWindow.updateUserGroup();
                MessageBox.Show("group was added");
                Close();

            }
            else
                MessageBox.Show("Empty fields");
        }
    }
}
