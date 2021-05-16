using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TestCreator
{
    /// <summary>
    /// Логика взаимодействия для CreateTest.xaml
    /// </summary>
    public partial class CreateTest : Window
    {
        static Question buffer_question;
        public CreateTest()
        {
            InitializeComponent();
        }

        private void Button_Click_AddQuestion(object sender, RoutedEventArgs e)
        {
            buffer_question = new Question();
            TextBox title = CreateElementUI.getTextBox(200, 20, new Thickness(0, 0, 0, 0), "Question",
                HorizontalAlignment.Left, VerticalAlignment.Top);
            Button buttonAdd = CreateElementUI.getButton(100, 30, new Thickness(0, 0, 0, 0), "ADD",
                 HorizontalAlignment.Left, VerticalAlignment.Top);
            buttonAdd.Click += Button_Click_AddAnswer;

            buffer_question.AnswersCheckBox = new List<CheckBox>();
            buffer_question.AnswersRadioButton = new List<RadioButton>();

            QuestionBox.Children.Add(title);
            buffer_question.QuestionTitle = title;

            for (int i = 0; i < 2; i++)
            {
                CheckBox ch = CreateElementUI.getCheckBox(100, 20, new Thickness(0, 0, 0, 0), "Answer #" + i,
                    HorizontalAlignment.Left, VerticalAlignment.Top);
                QuestionBox.Children.Add(ch);
                buffer_question.AnswersCheckBox.Add(ch);

            }
            buffer_question.AnswersRadioButton = CreateElementUI.toRadioButton(buffer_question.AnswersCheckBox);
            QuestionBox.Children.Add(buttonAdd);
        }


        private void Button_Click_AddAnswer(object sender, RoutedEventArgs e)
        {
            if (CheckBoxQuizMode.IsChecked == true)
            {
                RadioButton rb = CreateElementUI.getRadioButton(100, 20, new Thickness(0, 0, 0, 0), "Answer #" +
                    buffer_question.AnswersRadioButton.Count,
                  HorizontalAlignment.Left, VerticalAlignment.Top);
                QuestionBox.Children.Insert(QuestionBox.Children.Count - 1, rb);
                buffer_question.AnswersRadioButton.Add(rb);
            }
            else
            {
                CheckBox ch = CreateElementUI.getCheckBox(100, 20, new Thickness(0, 0, 0, 0), "Answer #" +
                       buffer_question.AnswersCheckBox.Count,
                   HorizontalAlignment.Left, VerticalAlignment.Top);
                QuestionBox.Children.Insert(QuestionBox.Children.Count - 1, ch);
                buffer_question.AnswersCheckBox.Add(ch);

            }
        }

        private void CheckBox_QuizMode_Click(object sender, RoutedEventArgs e)
        {

            if (CheckBoxQuizMode.IsChecked == true)
            {
                for (int i = 0; i < buffer_question.AnswersCheckBox.Count; i++)
                {
                    QuestionBox.Children.Remove(buffer_question.AnswersCheckBox[i]);
                }
                buffer_question.AnswersRadioButton = CreateElementUI.toRadioButton(buffer_question.AnswersCheckBox);
                for (int i = 0; i < buffer_question.AnswersRadioButton.Count; i++)
                {
                    QuestionBox.Children.Insert(i + 1, buffer_question.AnswersRadioButton[i]);
                }
            }
            else
            {
                for (int i = 0; i < buffer_question.AnswersRadioButton.Count; i++)
                {
                    QuestionBox.Children.Remove(buffer_question.AnswersRadioButton[i]);
                }
                buffer_question.AnswersCheckBox = CreateElementUI.toCheckBox(buffer_question.AnswersRadioButton);
                for (int i = 0; i < buffer_question.AnswersCheckBox.Count; i++)
                {
                    QuestionBox.Children.Insert(i + 1, buffer_question.AnswersCheckBox[i]);
                }
            }
        }

        private void Button_Click_SaveTest(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < QuestionBox.Children.Count; i++)
            {

            }
        }
    }
}
