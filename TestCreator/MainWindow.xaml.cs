using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace TestCreator
{
    public partial class MainWindow : Window
    {
       
        

        public MainWindow()
        {
            InitializeComponent();
            ClearGameField(null, null);
        }

        private void ClearGameField(object sender, RoutedEventArgs e)
        {
            Button button = new Button();
            button.Width = 100;
            button.Height = 100;
            button.Content = "B1";
            button.Click += ClearGameField;
            button.VerticalAlignment = VerticalAlignment.Top;
            button.HorizontalAlignment = HorizontalAlignment.Right;
            TestBox.Children.Add(button);
        }

      
    }
}
