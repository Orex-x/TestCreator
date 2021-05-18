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

namespace TestCreator
{

    public partial class QuestionFragment : UserControl
    {

        ObservableCollection<CheckBox> listCheckBox = new ObservableCollection<CheckBox>();
        ObservableCollection<RadioButton> listRadioButton = new ObservableCollection<RadioButton>();

        public QuestionFragment()
        {
            InitializeComponent();
            listAnswers.ItemsSource = listCheckBox;
        }


        private void Button_Click_AddAnswer(object sender, RoutedEventArgs e)
        {
            QuestionFragment qf = (sender as Button).DataContext as QuestionFragment;

            RadioButton rb = CreateElementUI.getRadioButton(100, 20, new Thickness(0, 0, 0, 0), "Answer #" +
                   listCheckBox.Count,
                 HorizontalAlignment.Left, VerticalAlignment.Top);

            CheckBox ch = CreateElementUI.getCheckBox(100, 20, new Thickness(0, 0, 0, 0), "Answer #" +
                     listCheckBox.Count,
                 HorizontalAlignment.Left, VerticalAlignment.Top);

            qf.listCheckBox.Add(ch);
            qf.listRadioButton.Add(rb);
        }

        private void CheckBox_QuizMode_Click(object sender, RoutedEventArgs e)
        {

            if (CheckBoxQuizMode.IsChecked == true)
            {
                listAnswers.ItemsSource = listRadioButton;
            }
            else
            {
                listAnswers.ItemsSource = listCheckBox;
            }
        }

      
    }
}
