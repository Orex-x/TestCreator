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
    /// Логика взаимодействия для ChooseTestWindow.xaml
    /// </summary>
    public partial class ChooseTestWindow : Window
    {
        public ObservableCollection<Test> tests = new ObservableCollection<Test>();
        UserGroup userGroup;
        GroupViewWindow groupViewWindow;
        public ChooseTestWindow()
        {
            InitializeComponent();
            listTests.ItemsSource = tests;
        }
        public void setData(UserGroup userGroup, GroupViewWindow groupViewWindow)
        {
            tests.Clear();
            this.userGroup = userGroup;
            this.groupViewWindow = groupViewWindow;

            foreach (Test test in userGroup.user.tests) tests.Add(test);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(listTests.SelectedItem != null)
            {
                Test test = listTests.SelectedItem as Test;
                bool exists = false;

                foreach(Test test2 in groupViewWindow.tests)
                {
                    if (test2.Equals(test))
                    {
                        exists = true;
                        break;
                    }   
                }
                if(!exists)
                {
                    groupViewWindow.tests.Add(test);
                    userGroup.group.tests.Add(test);
                    Client.updateGroup(userGroup.group, userGroup.group.id_group);
                }  
            }
        }
    }
}
