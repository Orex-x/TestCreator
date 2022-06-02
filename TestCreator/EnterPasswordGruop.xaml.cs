using System;
using System.Windows;
using TestCreator.Objects;
using TestCreator.TestCreatorAPI;

namespace TestCreator
{
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
            if (userGroup.group.password.Equals(passwordBox.Password))
            {
                Boolean add = Convert.ToBoolean(Client.addUserGroup(userGroup));
                if (add)
                {
                    MainWindow.groups.Add(userGroup);
                    MessageBox.Show("group add");
                }
                    
            }
            else
                MessageBox.Show("Invalid password");
        }
    }
}
