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

    public partial class MyProfileWindow : Window
    {
        public static ObservableCollection<Group> groups = new ObservableCollection<Group>();
        public static ObservableCollection<Test> tests = new ObservableCollection<Test>();
        public static ObservableCollection<PassedTest> passedTests = new ObservableCollection<PassedTest>();

        User user;
        public MyProfileWindow()
        {
            InitializeComponent();
            listTests.ItemsSource = tests;
            listPassetTests.ItemsSource = passedTests;
            listGroups.ItemsSource = groups;
        }

        public void init(User user)
        {
            this.user = user;

            firstName.Text = user.first_name;
            surname.Text = user.surname;
            email.Text = user.email;
            login.Text = user.login;
            patronymic.Text = user.last_name;

           

            ObservableCollection<Group> groups2 = Client.getGroupsByUser(user);
            ObservableCollection<Test> tests2 = user.tests;
            ObservableCollection<PassedTest> passedTests2 = user.passedTests;

            numberOfGroup.Content= groups2.Count;
            numberOfTests.Content = user.tests.Count;
            numberOfTestsSolved.Content = user.passedTests.Count;

            groups.Clear();
            tests.Clear();
            passedTests.Clear();

            foreach (Group group in groups2)
            {
                groups.Add(group);
            }
            foreach (PassedTest test in passedTests2)
            {
                passedTests.Add(test);
            }
            foreach (Test test in tests2)
            {
                tests.Add(test);
            }
        }

        private void Button_Click_AboutUser(object sender, RoutedEventArgs e)
        {
            listGroups.Visibility = Visibility.Hidden;
            listTests.Visibility = Visibility.Hidden;
            listPassetTests.Visibility = Visibility.Hidden;
            viewInfoPage(Visibility.Visible);
        }

        private void Button_Click_Group(object sender, RoutedEventArgs e)
        {
            listGroups.Visibility = Visibility.Visible;
            listTests.Visibility = Visibility.Hidden;
            listPassetTests.Visibility = Visibility.Hidden;
            viewInfoPage(Visibility.Hidden);
        }

        private void Button_Click_Test(object sender, RoutedEventArgs e)
        {
            listGroups.Visibility = Visibility.Hidden;
            listTests.Visibility = Visibility.Visible;
            listPassetTests.Visibility = Visibility.Hidden;
            viewInfoPage(Visibility.Hidden);
        }

        private void Button_Click_SolvedTets(object sender, RoutedEventArgs e)
        {
            listGroups.Visibility = Visibility.Hidden; 
            listTests.Visibility = Visibility.Hidden;
            listPassetTests.Visibility = Visibility.Visible;
            viewInfoPage(Visibility.Hidden);
        }

        public void viewInfoPage(Visibility visibility)
        {
            patronymic.Visibility = visibility;
            email.Visibility = visibility;
            login.Visibility = visibility;
            numberOfGroup.Visibility = visibility;
            numberOfTests.Visibility = visibility;
            numberOfTestsSolved.Visibility = visibility;
        }
    }
}
