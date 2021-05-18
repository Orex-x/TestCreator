using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
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

namespace TestCreator
{
    public partial class MainWindow : Window
    {
        public static ObservableCollection<Test> tests = new ObservableCollection<Test>();

        public MainWindow()
        {
            InitializeComponent();
            listTests.ItemsSource = tests;
            loadData();
        }

        private void Button_Click_AddTest(object sender, RoutedEventArgs e)
        {
            CreateTest createTest2 = new CreateTest();
            createTest2.Show();
        }

        private void Button_Click_EditTest(object sender, RoutedEventArgs e)
        {
            if(listTests.SelectedItem != null)
            {
                CreateTest createTest = new CreateTest();
                createTest.Show();
                createTest.loadTest(listTests.SelectedItem as Test);
            }
        }


        public void loadData()
        {
            List<string> testsJSON = new List<string>();
            DataBase.fillList(testsJSON, DataBase.FILE_TESTS);
            foreach (string testStr in testsJSON)
            {
                Test test = JsonSerializer.Deserialize<Test>(testStr);
                tests.Add(test);
            }
        }

        public void saveData()
        {
            List<string> testsJSON = new List<string>();
            foreach(Test test in tests)
            {
                string json = JsonSerializer.Serialize<Test>(test);
                testsJSON.Add(json);
            }
            DataBase.saveList(testsJSON, DataBase.FILE_TESTS);
        }

        private void Button_Click_SaveData(object sender, RoutedEventArgs e)
        {
            saveData();
        }

        private void Button_Click_ViewTest(object sender, RoutedEventArgs e)
        {
            if (listTests.SelectedItem != null)
            {
                ViewTest viewTest = new ViewTest();
                viewTest.Show();
                viewTest.loadTest(listTests.SelectedItem as Test);
            }

               
        }
    }
}
