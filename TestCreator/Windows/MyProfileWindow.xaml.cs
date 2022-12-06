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
        User parentUser;
        public MyProfileWindow()
        {
            InitializeComponent();
            listTests.ItemsSource = tests;
            listPassetTests.ItemsSource = passedTests;
            listGroups.ItemsSource = groups;
        }

        public void init(User user, User parentUser)
        {
            this.user = user;
            this.parentUser = parentUser;
            firstName.Text = user.first_name;
            surname.Text = user.surname;
            email.Text = user.email;
            login.Text = user.login;
            patronymic.Text = user.last_name;
            password.Password = user.password;
            Confirmpassword.Password = "";


            if (parentUser != null)
            {
                firstName.IsEnabled = false;
                surname.IsEnabled = false;
                email.IsEnabled = false;
                login.IsEnabled = false;
                patronymic.IsEnabled = false;
                firstName.IsEnabled = false;
                password.IsEnabled = false;
                Confirmpassword.IsEnabled = false;
                ButtonApplyEdit.IsEnabled = false;
            }

            ObservableCollection<Group> groups2 = Client.getGroupsByUser(user);
            ObservableCollection<Test> tests2 = user.tests;
            ObservableCollection<PassedTest> passedTests2 = user.passedTests;

            numberOfGroup.Content= "Number of group: " +  groups2.Count;
            numberOfTests.Content = "Number of tests: " + user.tests.Count;
            numberOfTestsSolved.Content = "Number of tests silved: " + user.passedTests.Count;

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
            lLogin.Visibility = visibility;
            lEmail.Visibility = visibility;
            password.Visibility = visibility;
            ButtonApplyEdit.Visibility = visibility;
            lPatronymic.Visibility = visibility;
            numberOfGroup.Visibility = visibility;
            numberOfTests.Visibility = visibility;
            numberOfTestsSolved.Visibility = visibility;
        }

        private void ButtonApplyEdit_Click(object sender, RoutedEventArgs e)
        {
            if (firstName.Text != user.first_name ||
                surname.Text != user.surname ||
                patronymic.Text != user.last_name ||
                email.Text != user.email)
            {
                if (!string.IsNullOrEmpty(firstName.Text))
                    user.first_name = firstName.Text;
                else MessageBox.Show("first name empty");

                if (!string.IsNullOrEmpty(surname.Text))
                    user.surname = surname.Text;
                else MessageBox.Show("surname empty");

                if (!string.IsNullOrEmpty(email.Text))
                    user.email = email.Text;
                else MessageBox.Show("email empty");

                if (!string.IsNullOrEmpty(patronymic.Text))
                    user.last_name = patronymic.Text;
                else MessageBox.Show("patronymic empty");

                Client.updateUser(user, user.id_user);
            }

            if (login.Text != user.login)
            {

                if (!string.IsNullOrEmpty(login.Text))
                {
                    string s = Client.userLoginIsBusy(login.Text);
                    if (s == "false")
                        user.login = login.Text;
                    else
                        MessageBox.Show("login is busy");
                }
                else MessageBox.Show("login empty");
            }

            if (password.Password != user.password)
            {
                if(Confirmpassword.Password != password.Password)
                {
                    Confirmpassword.Visibility = Visibility.Visible;
                }
                else
                {
                    Confirmpassword.Visibility = Visibility.Hidden;
                    Confirmpassword.Password = "";
                    user.password = password.Password;
                    Client.updateUser(user, user.id_user);
                }
            }
           
        }

        private void listTests_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listTests.SelectedItem != null)
            {
                ViewTest window = new ViewTest();
                if (parentUser == null)
                    window.init(user);
                else
                    window.init(parentUser);
                window.Show();
                MainWindow.winds.Add(window);
                window.loadTest(listTests.SelectedItem as Test);
            }
        }

        private void listPassetTests_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listPassetTests.SelectedItem != null)
            {
                ViewTest window = new ViewTest();
                if (parentUser == null)
                    window.init(user);
                else
                    window.init(parentUser);
                window.loadTest(listPassetTests.SelectedItem as PassedTest);
                MainWindow.winds.Add(window);
                window.Show();
            }
        }
    }
}