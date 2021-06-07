using System;
using System.Collections.ObjectModel;
using System.Windows;
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
        private User user;

        public SearchGroup()
        {
            InitializeComponent();
            updateGroupList();
            listGroup.ItemsSource = groups;
        }
        public void init(User user)
        {
            this.user = user;
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
                        user = user,
                        group = group,
                        is_admin = false
                    };
                    if (group.security_status == Group.Security.Private)
                    {
                        EnterPasswordGruop window = new EnterPasswordGruop();
                        window.init(userGroup);
                        MainWindow.winds.Add(window);
                        window.Show();
                    }
                    else
                    {
                        Boolean add = Convert.ToBoolean(Client.addUserGroup(userGroup));
                        if (add)
                            MainWindow.groups.Add(userGroup);
                    } 
                }
                else
                    MessageBox.Show("invalid invitation link");

            }
            else if(listGroup.SelectedItem != null)
            {
                Group group = listGroup.SelectedItem as Group;
                UserGroup userGroup = new UserGroup
                {
                    user = user,
                    group = group,
                    is_admin = false
                };
                EnterPasswordGruop window = new EnterPasswordGruop();
                window.init(userGroup);
                MainWindow.winds.Add(window);
                window.Show();
            }
        }
    }
}
