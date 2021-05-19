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
using TestCreator.Objects;

namespace TestCreator
{
    /// <summary>
    /// Логика взаимодействия для ViewTest.xaml
    /// </summary>
    public partial class ViewTest : Window
    {

        List<TestViewer> testViewers = new List<TestViewer>();

        Test Maintest;

        public ViewTest()
        {
            InitializeComponent();
            testViewers.Clear();
            listViewQuestions.ItemsSource = testViewers;
        }
        

        //почему если вопросак два он лашает
        public void loadTest(Test test)
        {
            Maintest = test;
            labelTitleTest.Content = test.Title;
            foreach (Question questions in test.Questions)
            {
                TestViewer tw = new TestViewer();
                tw.labelThemeQuestion.Content = questions.Title;

                if (questions.IsCheckBox)
                {
                    tw.listAnswers.ItemsSource = tw.checkBoxesAnswers;
                    for (int i = 0; i < questions.Answers.Count; i++)
                    {
                        Label l = new Label();
                        l.Content = questions.Answers[i].Title;
                        CheckBox ch = CreateElementUI.getCheckBox(100, 20, new Thickness(0, 0, 0, 0), false, l,
                            HorizontalAlignment.Left, VerticalAlignment.Top);
                        tw.checkBoxesAnswers.Add(ch);
                    }
                }
                else
                {
                    tw.listAnswers.ItemsSource = tw.radioButtonsAnswers;
                    for (int i = 0; i < questions.Answers.Count; i++)
                    {
                        Label l = new Label();
                        l.Content = questions.Answers[i].Title;

                        RadioButton rb = CreateElementUI.getRadioButton(100, 20, new Thickness(0, 0, 0, 0),
                            false, l, questions.Answers[i].GroupName, HorizontalAlignment.Left, VerticalAlignment.Top);
                        tw.radioButtonsAnswers.Add(rb);
                    }
                }
                testViewers.Add(tw);
            }
        }

        private void Button_Click_SendTest(object sender, RoutedEventArgs e)
        {
            double count = 0;
            for(int i = 0; i < Maintest.Questions.Count; i++)
            {
                Question question = Maintest.Questions[i];
                for (int j = 0; j < question.Answers.Count; j++)
                {
                    Answer trueAnswer = question.Answers[j];
                    if (question.IsCheckBox)
                    {
                        CheckBox ch = testViewers[i].listAnswers.Items[j] as CheckBox;
                        if (trueAnswer.IsTrue && ch.IsChecked == trueAnswer.IsTrue)
                        {
                            double m = (double) Maintest.Questions[i].Mark / 
                                (double) Maintest.Questions[i].NumTrueAnswer;
                            count += m;
                        }
                    }
                    else
                    {
                        RadioButton rd = testViewers[i].listAnswers.Items[j] as RadioButton;
                        if (trueAnswer.IsTrue && rd.IsChecked == trueAnswer.IsTrue)
                        {
                            count += Maintest.Questions[i].Mark;
                        }
                    }
                }
            }
            MessageBox.Show("Вы небрали " + count + " баллов");

        }

        public void saveAnswers()
        {
            for(int i = 0; i < testViewers.Count; i++)
            {
                 
            }
        }
    }


   
}
