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
    /// Логика взаимодействия для GroupViewWindow.xaml
    /// </summary>
    public partial class GroupViewWindow : Window
    {
        public UserGroup mainUserGroup;
        public ObservableCollection<User> subscribers = new ObservableCollection<User>();
        public ObservableCollection<Test> tests = new ObservableCollection<Test>();

        public GroupViewWindow()
        {
            InitializeComponent();
            listSubscribers.ItemsSource = subscribers;
            listTests.ItemsSource = tests;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (mainUserGroup.is_admin)
            {
                Client.deleteGroup(mainUserGroup.group.id_group);
            }
            else
            {
                long id = Client.getIdByUserAndGroup(mainUserGroup);
                Client.deleteuserGroup(id);
            }
            MainWindow.groups.Remove(mainUserGroup);
            Close();
        }

        public void showLink(String link)
        {
            invitationLink.Content = "Invitation link: " + link;
            invitationLink.Visibility = Visibility.Visible;
            copyLink.Visibility = Visibility.Visible;
            button_publish_test.Visibility = Visibility.Visible;
            deleteGroup.Content = "delete group";
        }

        private void copyLink_Click_CopyLink(object sender, RoutedEventArgs e)
        {
            try
            {
                Clipboard.SetText(mainUserGroup.group.invitationLink);
            }
            catch(Exception ww) {
                MessageBox.Show(ww.ToString());
            }
            
        }

        private void listSubscribers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(listSubscribers.SelectedItem != null)
            {
                User user = listSubscribers.SelectedItem as User;
                UserViewWindow userViewWindow = new UserViewWindow();
                userViewWindow.set_info(user);
                userViewWindow.Show();
            }
           
        }

        private void Button_Click_addTest(object sender, RoutedEventArgs e)
        {
            ChooseTestWindow testWindow = new ChooseTestWindow();
            testWindow.setData(mainUserGroup, this);
            testWindow.Show();
        }

        private void delete_test_from_group_Click(object sender, RoutedEventArgs e)
        {
            if (listTests.SelectedItem != null)
            {
                Test test = listTests.SelectedItem as Test;
                tests.Remove(test);
                mainUserGroup.group.tests.Remove(test);
                Client.updateGroup(mainUserGroup.group, mainUserGroup.group.id_group);
            }
        }
    }
}
