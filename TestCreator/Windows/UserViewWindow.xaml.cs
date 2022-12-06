using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using TestCreator.Objects;
using TestCreator.TestCreatorAPI;

namespace TestCreator
{
    public partial class UserViewWindow : Window
    {
        public static ObservableCollection<Group> groups = new ObservableCollection<Group>();
        public static ObservableCollection<Test> tests = new ObservableCollection<Test>();
        public static ObservableCollection<PassedTest> passedTests = new ObservableCollection<PassedTest>();
        private User user;

        public UserViewWindow()
        {
            InitializeComponent();
            listGroups.ItemsSource = groups;
            listTests.ItemsSource = tests;
            listPassetTests.ItemsSource = passedTests;
        }

        public void set_info(User user)
        {
            firstName.Content = user.first_name;
            lastName.Content = user.last_name;
            email.Content = user.email;
            labelTests.Content = user.first_name + "'s tests";

            ObservableCollection<Group> groups2 = Client.getGroupsByUser(user);
            ObservableCollection<Test> tests2 = user.tests;
            ObservableCollection<PassedTest> passedTests2 = user.passedTests; 
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

        public void viewEditProfile()
        {
            firstName.Visibility = Visibility.Hidden;
            lastName.Visibility = Visibility.Hidden;
            email.Visibility = Visibility.Hidden;

            textBoxfirstName.Visibility = Visibility.Visible;
            textBoxlastName.Visibility = Visibility.Visible;
            textBoxemail.Visibility = Visibility.Visible;
        }
    }
}
