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
    /// Логика взаимодействия для MyTestsPage.xaml
    /// </summary>
    public partial class MyTestsPage : Page
    {

        public static ObservableCollection<Test> tests = new ObservableCollection<Test>();
        public MyTestsPage()
        {
            InitializeComponent();

            listTests.ItemsSource = tests;

            UpdateTests();
        }

        public static void UpdateTests()
        {
            StaticObject.mainUser.tests = Client.getTestsByUser(StaticObject.mainUser);
            tests.Clear();
            foreach (var test in StaticObject.mainUser.tests)
            {
                tests.Add(test);
            }
        }


        private void Button_Click_AddTest(object sender, RoutedEventArgs e)
        {
            var win = new CreateTest();
            win.Show();
        }

        private void Button_Click_Edit_Test(object sender, RoutedEventArgs e)
        {
            if (listTests.SelectedItem != null)
            {
                var test = listTests.SelectedItem as Test;

                if (Client.findGroupTestByTest(test) == "false")
                {
                    var window = new CreateTest();
                    window.Show();
                    window.loadTest(test);
                }
                else
                {
                    MessageBox.Show("published test cannot be edited");
                }
            }
        }

        private void Button_Click_Delete_Test(object sender, RoutedEventArgs e)
        {
            if (listTests.SelectedItem != null)
            {
                var test = listTests.SelectedItem as Test;
                StaticObject.mainUser.tests.Remove(test);
                Client.updateUser(StaticObject.mainUser, StaticObject.mainUser.id_user);
                Client.deleteTest(test.idTest);
                tests.Remove(test);
            }
        }

        private void List_Tests_Mouse_Double_Click(object sender, MouseButtonEventArgs e)
        {
            Button_Click_Edit_Test(null, null);
        }

    }
}
