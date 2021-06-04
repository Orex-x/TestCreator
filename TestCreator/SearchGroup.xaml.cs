using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для SearchGroup.xaml
    /// </summary>
    public partial class SearchGroup : Window
    {

        ObservableCollection<Group> groups = new ObservableCollection<Group>();

        public SearchGroup()
        {
            InitializeComponent();
            updateGroupList();
            listGroup.ItemsSource = groups;
        }

        private void Button_ClickUpdate(object sender, RoutedEventArgs e)
        {
            updateGroupList();
        }

        private void updateGroupList()
        {
            groups.Clear();
            foreach (Group g in Client.getAllGroup())
            {
                if(g.security_status == Group.Security.Public)
                    groups.Add(g);
            }
        }

        private void Button_Click_Join(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(invitationLink.Text))
            {
                Group group = Client.joinTheGroupByLink(invitationLink.Text);
                if (group != null)
                {
                    UserGroup userGroup = new UserGroup
                    {
                        user = MainWindow.mainUser,
                        group = group,
                        is_admin = false
                    };
                    Boolean add = Convert.ToBoolean(Client.addUserGroup(userGroup));
                    if (add)
                        MainWindow.groups.Add(userGroup);
                }
                else
                    MessageBox.Show("invalid invitation link");


            }
            else if(listGroup.SelectedItem != null)
            {
                Group group = listGroup.SelectedItem as Group;
                UserGroup userGroup = new UserGroup
                {
                    user = MainWindow.mainUser,
                    group = group,
                    is_admin = false
                };
                Boolean add = Convert.ToBoolean(Client.addUserGroup(userGroup)); 
                if(add)
                    MainWindow.groups.Add(userGroup);
            }
        }
    }
}
