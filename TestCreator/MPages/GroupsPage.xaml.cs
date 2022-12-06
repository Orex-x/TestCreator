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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestCreator.Objects;
using TestCreator.Service;
using TestCreator.TestCreatorAPI;

namespace TestCreator.MPages
{
    /// <summary>
    /// Логика взаимодействия для GroupsPage.xaml
    /// </summary>
    public partial class GroupsPage : Page
    {
        public static ObservableCollection<UserGroup> groups = new ObservableCollection<UserGroup>();


        public GroupsPage()
        {
            InitializeComponent();

            listGroups.ItemsSource = groups;

            updateUserGroup();
        }

        public static void updateUserGroup()
        {
            groups.Clear();
            var groups2 = Client.getUserGroupsByUser(StaticObject.mainUser);
            foreach (var UserGroup in groups2)
            {
                groups.Add(UserGroup);
            }
        }

        private void List_Groups_Mouse_Double_Click(object sender, MouseButtonEventArgs e)
        {

            var listView = (ListView)sender;
            if (listView.SelectedItem != null)
            {
                var userGroup = listView.SelectedItem as UserGroup;
                var window = new GroupViewWindow();
                window.init(userGroup);
                StaticObject.winds.Add(window);
                window.Show();
            }
        }

        private void Button_Click_Search_Group(object sender, RoutedEventArgs e)
        {
            SearchGroup window = new SearchGroup();
            window.init(StaticObject.mainUser);
            window.Show();
            StaticObject.winds.Add(window);
        }

        private void Button_Click_Add_Group(object sender, RoutedEventArgs e)
        {
            AddGroup window = new AddGroup();
            window.init(StaticObject.mainUser);
            window.Show();
            StaticObject.winds.Add(window);
        }

    }
}
