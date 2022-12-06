using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using TestCreator.MPages;
using TestCreator.Objects;
using TestCreator.Service;
using TestCreator.TestCreatorAPI;

namespace TestCreator.Windows
{
    public partial class HomeWindow : Window
    {
       


        private void ShowCloseMenu(object sender, RoutedEventArgs e)
        {
            var animation = new DoubleAnimation();
            animation.From = leftGrid.ActualWidth;
            if (leftGrid.ActualWidth == 50)
                animation.To = 250;
            else
                animation.To = 50;
            animation.AccelerationRatio = 1;
            animation.Duration = TimeSpan.FromSeconds(0.1);
            leftGrid.BeginAnimation(StackPanel.WidthProperty, animation);
        }

        public HomeWindow()
        {
            InitializeComponent();
            Button_Click_Open_My_Profile_Page(null, null);
        }


        public void setPage(object obj)
        {
            frame.Navigate(obj);
        }

      

        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            foreach (var w in StaticObject.winds) w.Close();
            
            var signInWindow = new SignInWindow();
            signInWindow.Show();
            
            Database.DeleteFile(Database.FILE_LOG);
            Close();
        }

        private void Button_Click_Open_My_Tests(object sender, RoutedEventArgs e)
        {
            txtTitle.Text = "Мои тесты";
            var page = new MyTestsPage();
            setPage(page);
        }

        private void Button_Click_Open_My_Profile_Page(object sender, RoutedEventArgs e)
        {
            txtTitle.Text = "Мой профиль";
            var page = new ProfilePage();
            page.init(null);
            setPage(page);
        } 
        
        private void Button_Click_Open_Solved_Tests_Page(object sender, RoutedEventArgs e)
        {
            txtTitle.Text = "Решенные тесты";
            var page = new SolvedTestsPage();
            setPage(page);
        } 
        
        private void Button_Click_Open_Groups_Page(object sender, RoutedEventArgs e)
        {
            txtTitle.Text = "Группы";
            var page = new GroupsPage();
            setPage(page);
        }
    }
}
