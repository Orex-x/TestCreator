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
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public static ObservableCollection<Group> groups = new ObservableCollection<Group>();
        public static ObservableCollection<Test> tests = new ObservableCollection<Test>();
        public static ObservableCollection<PassedTest> passedTests = new ObservableCollection<PassedTest>();

        private User _user;
        private User _parentUser;
        public ProfilePage()
        {
            InitializeComponent();
            listTests.ItemsSource = tests;
            listPassetTests.ItemsSource = passedTests;
            listGroups.ItemsSource = groups;
        }

        public void init(User parentUser)
        {
            _user = StaticObject.mainUser;
            _parentUser = parentUser;

            firstName.Text = _user.first_name;
            surname.Text = _user.surname;
            email.Text = _user.email;
            login.Text = _user.login;
            patronymic.Text = _user.last_name;
            password.Password = _user.password;
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

            var groups2 = Client.getGroupsByUser(_user);
            var tests2 = _user.tests;
            var passedTests2 = _user.passedTests;

            numberOfGroup.Content = "Number of group: " + groups2.Count;
            numberOfTests.Content = "Number of tests: " + _user.tests.Count;
            numberOfTestsSolved.Content = "Number of tests silved: " + _user.passedTests.Count;

            groups.Clear();
            tests.Clear();
            passedTests.Clear();

            foreach (var group in groups2)
                groups.Add(group);
            
            foreach (var test in passedTests2)
                passedTests.Add(test);
            
            foreach (var test in tests2)
                tests.Add(test); 
        }


        private void ButtonApplyEdit_Click(object sender, RoutedEventArgs e)
        {
            if (firstName.Text != _user.first_name ||
                surname.Text != _user.surname ||
                patronymic.Text != _user.last_name ||
                email.Text != _user.email)
            {
                if (!string.IsNullOrEmpty(firstName.Text))
                    _user.first_name = firstName.Text;
                else MessageBox.Show("first name empty");

                if (!string.IsNullOrEmpty(surname.Text))
                    _user.surname = surname.Text;
                else MessageBox.Show("surname empty");

                if (!string.IsNullOrEmpty(email.Text))
                    _user.email = email.Text;
                else MessageBox.Show("email empty");

                if (!string.IsNullOrEmpty(patronymic.Text))
                    _user.last_name = patronymic.Text;
                else MessageBox.Show("patronymic empty");

                Client.updateUser(_user, _user.id_user);
            }

            if (login.Text != _user.login)
            {

                if (!string.IsNullOrEmpty(login.Text))
                {
                    string s = Client.userLoginIsBusy(login.Text);
                    if (s == "false")
                        _user.login = login.Text;
                    else
                        MessageBox.Show("login is busy");
                }
                else MessageBox.Show("login empty");
            }

            if (password.Password != _user.password)
            {
                if (Confirmpassword.Password != password.Password)
                {
                    Confirmpassword.Visibility = Visibility.Visible;
                }
                else
                {
                    Confirmpassword.Visibility = Visibility.Hidden;
                    Confirmpassword.Password = "";
                    _user.password = password.Password;
                    Client.updateUser(_user, _user.id_user);
                }
            }

        }

        private void listTests_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listTests.SelectedItem != null)
            {
                var window = new ViewTest();
                if (_parentUser == null)
                    window.init(_user);
                else
                    window.init(_parentUser);
                window.Show();
                MainWindow.winds.Add(window);
                window.loadTest(listTests.SelectedItem as Test);
            }
        }

        private void listPassetTests_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listPassetTests.SelectedItem != null)
            {
                var window = new ViewTest();
                if (_parentUser == null)
                    window.init(_user);
                else
                    window.init(_parentUser);
                window.loadTest(listPassetTests.SelectedItem as PassedTest);
                MainWindow.winds.Add(window);
                window.Show();
            }
        }
    }
}
