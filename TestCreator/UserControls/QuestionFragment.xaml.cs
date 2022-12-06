using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using TestCreator.Service;

namespace TestCreator
{

    public partial class QuestionFragment : UserControl
    {

        public ObservableCollection<CheckBox> listCheckBox = new ObservableCollection<CheckBox>();
        public ObservableCollection<RadioButton> listRadioButton = new ObservableCollection<RadioButton>();

        public QuestionFragment()
        {
            InitializeComponent();
            listAnswers.ItemsSource = listCheckBox;
        }

        private void Button_Click_AddAnswer(object sender, RoutedEventArgs e)
        {
            QuestionFragment qf = (sender as Button).DataContext as QuestionFragment;

            TextBox box1 = new TextBox();
            box1.Text = "Answer #" + listCheckBox.Count;
            TextBox box2 = new TextBox();
            box2.Text = "Answer #" + listCheckBox.Count;

            RadioButton rb = CreateElementUI.getRadioButton(35, new Thickness(0, 0, 0, 0), false, box1, CreateTest.listQustions.IndexOf(qf).ToString(),
                 HorizontalAlignment.Left, VerticalAlignment.Top);

            CheckBox ch = CreateElementUI.getCheckBox( 35, new Thickness(0, 0, 0, 0), false, box2,
                 HorizontalAlignment.Left, VerticalAlignment.Top);

            qf.listCheckBox.Add(ch);
            qf.listRadioButton.Add(rb);
        }

        private void CheckBox_QuizMode_Click(object sender, RoutedEventArgs e)
        {
            updateLists();
        }

        public void updateLists()
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

        private void Button_Click_Delete_Question(object sender, RoutedEventArgs e)
        {
            QuestionFragment qf = (sender as Button).DataContext as QuestionFragment;
            if (qf.listAnswers.SelectedItem != null)
            {
                if(qf.CheckBoxQuizMode.IsChecked == true)
                {
                    listRadioButton.Remove(qf.listAnswers.SelectedItem as RadioButton);
                }
                else
                {
                    listCheckBox.Remove(qf.listAnswers.SelectedItem as CheckBox);
                }
            } 
        }
    }
}
