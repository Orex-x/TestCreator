using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TestCreator.Objects;
using TestCreator.TestCreatorAPI;

namespace TestCreator
{
    public partial class MainWindow : Window
    {
        public static ObservableCollection<Test> tests = new ObservableCollection<Test>();
        public static ObservableCollection<UserGroup> groups = new ObservableCollection<UserGroup>();
        public static ObservableCollection<PassedTest> passedTests = new ObservableCollection<PassedTest>();
        public static List<Window> winds = new List<Window>();
        private static User mainUser;

        public MainWindow()
        {
            InitializeComponent();
            listTests.ItemsSource = tests;
            listGroups.ItemsSource = groups;
            listPassedTests.ItemsSource = passedTests;
            Button_MyTests_Click(null, null);
        }

        public void init(User user)
        {
            mainUser = user;
            updatePassedTest();
            updateUserGroup();
            updateTest();
        }

        public static void updateUserGroup()
        {
            groups.Clear();
            ObservableCollection<UserGroup> groups2 = Client.getUserGroupsByUser(mainUser);
            foreach (UserGroup UserGroup in groups2)
            {
                groups.Add(UserGroup);
            }
        }

        public static void updatePassedTest()
        {
            passedTests.Clear();
            mainUser.passedTests = Client.getPassedestsByUser(mainUser);
            foreach (PassedTest p in mainUser.passedTests)
            {
                passedTests.Add(p);
            }
        }

        public static void updateTest()
        {
            mainUser.tests = Client.getTestsByUser(mainUser);
            tests.Clear();
            foreach (Test test in mainUser.tests)
            {
                tests.Add(test);
            }
        }

        private void Button_Click_AddTest(object sender, RoutedEventArgs e)
        {
            CreateTest createTest2 = new CreateTest();
            createTest2.init(mainUser);
            createTest2.Show();
        }

        private void Button_Click_EditTest(object sender, RoutedEventArgs e)
        {
            if(listTests.SelectedItem != null)
            {
                Test test = listTests.SelectedItem as Test;

                if(Client.findGroupTestByTest(test) == "false")
                {
                    CreateTest window = new CreateTest();
                    window.init(mainUser);
                    window.Show();
                    winds.Add(window);
                    window.loadTest(test);
                }
                else
                {
                    MessageBox.Show("published test cannot be edited");
                }
            }
        }

        private void Button_Click_ViewTest(object sender, RoutedEventArgs e)
        {
            if (listTests.SelectedItem != null)
            {
                ViewTest window = new ViewTest();
                window.init(mainUser);
                window.Show();
                winds.Add(window);
                window.loadTest(listTests.SelectedItem as Test);
            }
        }

        private void Button_ClickSearchGroup(object sender, RoutedEventArgs e)
        {
            SearchGroup window = new SearchGroup();
            window.init(mainUser);
            window.Show();
            winds.Add(window);
        }

        private void Button_ClickAddGroup(object sender, RoutedEventArgs e)
        {
            AddGroup window = new AddGroup();
            window.init(mainUser);
            window.Show();
            winds.Add(window);
        }

        private void listGroups_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           
            ListView listView = (ListView)sender;
            if (listView.SelectedItem != null)
            {
                UserGroup userGroup = listView.SelectedItem as UserGroup;
                GroupViewWindow window = new GroupViewWindow();
                window.init(userGroup);
                winds.Add(window);
                window.Show();
            }
        }

        private void Button_ClickDeleteTest(object sender, RoutedEventArgs e)
        {
            if (listTests.SelectedItem != null)
            {
                Test test = listTests.SelectedItem as Test;
                mainUser.tests.Remove(test);
                Client.updateUser(mainUser, mainUser.id_user);
                Client.deleteTest(test.idTest);
                tests.Remove(test);
            }
        }

        private void listPassedTests_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listPassedTests.SelectedItem != null)
            {
                ViewTest window = new ViewTest();
                window.init(mainUser);
                window.loadTest(listPassedTests.SelectedItem as PassedTest);
                winds.Add(window);
                window.Show();
            }
        }
        private void Button_Click_OpenMyProfile(object sender, RoutedEventArgs e)
        {
            MyProfileWindow window = new MyProfileWindow();
            window.init(mainUser, null);
            window.Show();
            winds.Add(window);

        }

        private void Button_MyTests_Click(object sender, RoutedEventArgs e)
        {
            listTests.Visibility = Visibility.Visible;
            listPassedTests.Visibility = Visibility.Hidden;
            listGroups.Visibility = Visibility.Hidden;

            ButtonAddTest.Visibility = Visibility.Visible;
            ButtonEditTest.Visibility = Visibility.Visible;
            ButtonDeleteTest.Visibility = Visibility.Visible;

            ButtonSearchGroup.Visibility = Visibility.Hidden;
            ButtonAddGroup.Visibility = Visibility.Hidden;
        }

        private void Button_SolvedTests_Click(object sender, RoutedEventArgs e)
        {
            listTests.Visibility = Visibility.Hidden;
            listPassedTests.Visibility = Visibility.Visible;
            listGroups.Visibility = Visibility.Hidden;

            ButtonAddTest.Visibility = Visibility.Hidden;
            ButtonEditTest.Visibility = Visibility.Hidden;
            ButtonDeleteTest.Visibility = Visibility.Hidden;

            ButtonSearchGroup.Visibility = Visibility.Hidden;
            ButtonAddGroup.Visibility = Visibility.Hidden;
        }

        private void Button_Groups_Click(object sender, RoutedEventArgs e)
        {
            listTests.Visibility = Visibility.Hidden;
            listPassedTests.Visibility = Visibility.Hidden;
            listGroups.Visibility = Visibility.Visible;


            ButtonAddTest.Visibility = Visibility.Hidden;
            ButtonEditTest.Visibility = Visibility.Hidden;
            ButtonDeleteTest.Visibility = Visibility.Hidden;

            ButtonSearchGroup.Visibility = Visibility.Visible;
            ButtonAddGroup.Visibility = Visibility.Visible;
        }

        private void listTests_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Button_Click_EditTest(null, null);
        }

        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            
            foreach(Window w in winds)
            {
                w.Close();
            }
            SignInWindow signInWindow = new SignInWindow();
            signInWindow.Show();
            Database.DeleteFile(Database.FILE_LOG);
            this.Close(); 
        }
    }
}