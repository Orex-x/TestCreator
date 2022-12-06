using System;
using System.Windows;
using TestCreator.Objects;
using TestCreator.TestCreatorAPI;

namespace TestCreator
{
    public partial class AddGroup : Window
    {
        private User user;
        public AddGroup()
        {
            InitializeComponent();
        }

        public void init(User user)
        {
            this.user = user;
        }

        private void Button_Click_AddGroup(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(group_name.Text) && 
                !string.IsNullOrEmpty(group_password.Password) &&
                  !string.IsNullOrEmpty(group_comfirm_password.Password))
            {
                if (group_comfirm_password.Password == group_password.Password)
                {
                    Group Addgroup = new Group
                    {
                        name = group_name.Text,
                        password = group_password.Password
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
                        user = user
                    };

                    Client.addUserGroup(userGroup);
                    MainWindow.updateUserGroup();
                    MessageBox.Show("group was added");
                    Close();
                }
                else
                    MessageBox.Show("Password mismatch");
            }
            else
                MessageBox.Show("Empty fields");
        }
    }
}
