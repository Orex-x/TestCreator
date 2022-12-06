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
    /// Логика взаимодействия для SolvedTestsPage.xaml
    /// </summary>
    public partial class SolvedTestsPage : Page
    {
        public static ObservableCollection<PassedTest> passedTests = new ObservableCollection<PassedTest>();


        public SolvedTestsPage()
        {
            InitializeComponent();

            listPassedTests.ItemsSource = passedTests;

            updatePassedTest();
        }

        public static void updatePassedTest()
        {
            passedTests.Clear();
            StaticObject.mainUser.passedTests = Client.getPassedestsByUser(StaticObject.mainUser);
            foreach (var p in StaticObject.mainUser.passedTests)
            {
                passedTests.Add(p);
            }
        }


        private void List_Passed_Tests_Mouse_Double_Click(object sender, MouseButtonEventArgs e)
        {
            var test = listPassedTests.SelectedItem as PassedTest;
            if (test != null)
            {
                var window = new ViewTest();
                window.init(StaticObject.mainUser);
                window.loadTest(test);
                StaticObject.winds.Add(window);
                window.Show();
            }
        }
    }
}
