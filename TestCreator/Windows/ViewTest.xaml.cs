using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TestCreator.Objects;
using TestCreator.Service;
using TestCreator.TestCreatorAPI;

namespace TestCreator
{
    public partial class ViewTest : Window
    {

        List<TestViewer> testViewers = new List<TestViewer>();
        private Test test;
        private PassedTest passedTest;
        private User user;

        public ViewTest()
        {
            InitializeComponent();
            testViewers.Clear();
            listViewQuestions.ItemsSource = testViewers;
        }

        public void init(User user)
        {
            this.user = user;
        }


        public void loadTest(PassedTest passedTest)
        {
            sendTest.Visibility = Visibility.Hidden;
            Result.Visibility = Visibility.Visible;
            Result.Content = "Result: " + passedTest.result;
            this.passedTest = passedTest;
            labelTitleTest.Content = passedTest.test.title;

            foreach(Question question in passedTest.test.questions)
            {
                TestViewer tw = new TestViewer();
                tw.labelThemeQuestion.Content = question.title;

                List<string> passedAnswers = getPassedAnswersByQuestion(question, passedTest);

                if (question.isCheckBox)
                {
                    tw.listAnswers.ItemsSource = tw.checkBoxesAnswers;
                    for (int i = 0; i < question.answers.Count; i++)
                    {
                        Label l = new Label();
                        Answer answer = question.answers[i];
                        if (answer.isTrue)
                            l.Background = new SolidColorBrush(Color.FromArgb(90, 0xF0, 0x00, 0xFF));

                        l.Content = answer.title;
                        CheckBox ch = CreateElementUI.getCheckBox(35, new Thickness(0, 0, 0, 0), false, l,
                            HorizontalAlignment.Left, VerticalAlignment.Top);
                        ch.IsEnabled = false;   
                        

                        if(passedAnswers.Contains(answer.title))
                            ch.IsChecked = true;

                        tw.checkBoxesAnswers.Add(ch);
                    }
                }
                else
                {
                    tw.listAnswers.ItemsSource = tw.radioButtonsAnswers;
                    for (int i = 0; i < question.answers.Count; i++)
                    {
                        Label l = new Label();
                        Answer answer = question.answers[i];
                        if (answer.isTrue)
                            l.Background = new SolidColorBrush(Color.FromArgb(90, 0xF0, 0x00, 0xFF));
                        l.Content = answer.title;

                        RadioButton rb = CreateElementUI.getRadioButton(35, new Thickness(0, 0, 0, 0),
                            false, l, answer.groupName, HorizontalAlignment.Left, VerticalAlignment.Top);

                        rb.IsEnabled = false;

                        if (passedAnswers.Contains(answer.title))
                            rb.IsChecked = true;

                        tw.radioButtonsAnswers.Add(rb);
                    }
                }
                testViewers.Add(tw);
            }


        }
        public static List<string> getPassedAnswersByQuestion(Question question,PassedTest passedTest)
        {
            List<string> passedAnswers = new List<string>();
            foreach(PassedAnswer passedAnswer in passedTest.passedAnswers)
            {
                if(passedAnswer.question.title == question.title)
                    passedAnswers.Add(passedAnswer.title);
            }
            return passedAnswers;
        }
        //почему если вопросак два он лашает
        public void loadTest(Test test)
        {
            this.test = test;
            labelTitleTest.Content = test.title;
            foreach (Question questions in test.questions)
            {
                TestViewer tw = new TestViewer();
                tw.labelThemeQuestion.Content = questions.title;

                if (questions.isCheckBox)
                {
                    tw.listAnswers.ItemsSource = tw.checkBoxesAnswers;
                    for (int i = 0; i < questions.answers.Count; i++)
                    {
                        Label l = new Label();
                        l.Content = questions.answers[i].title;
                        CheckBox ch = CreateElementUI.getCheckBox( 35, new Thickness(0, 0, 0, 0), false, l,
                            HorizontalAlignment.Left, VerticalAlignment.Top);
                        tw.checkBoxesAnswers.Add(ch);
                    }
                }
                else
                {
                    tw.listAnswers.ItemsSource = tw.radioButtonsAnswers;
                    for (int i = 0; i < questions.answers.Count; i++)
                    {
                        Label l = new Label();
                        l.Content = questions.answers[i].title;

                        RadioButton rb = CreateElementUI.getRadioButton(35, new Thickness(0, 0, 0, 0),
                            false, l, questions.answers[i].groupName, HorizontalAlignment.Left, VerticalAlignment.Top);
                        tw.radioButtonsAnswers.Add(rb);
                    }
                }
                testViewers.Add(tw);
            }
        }

        private void Button_Click_SendTest(object sender, RoutedEventArgs e)
        {
            double count = 0;
            double countIsTrue = 0;
            PassedTest passedTest = new PassedTest
            {
                test = test,
                passedAnswers = new ObservableCollection<PassedAnswer>()
            };


            for (int i = 0; i < test.questions.Count; i++)
            {
                Question question = test.questions[i];
                for (int j = 0; j < question.answers.Count; j++)
                {
                    Answer answer = question.answers[j];
                    PassedAnswer passedAnswer = new PassedAnswer()
                    {
                        question = question
                    };

                    if (answer.isTrue)
                        countIsTrue += question.mark / question.numTrueAnswer;

                    if (question.isCheckBox)
                    {
                        CheckBox ch = testViewers[i].listAnswers.Items[j] as CheckBox;


                        string a = (ch.Content as Label).Content.ToString();
                        passedAnswer.title = a;

                        if (ch.IsChecked == true)
                            passedTest.passedAnswers.Add(passedAnswer);

                        if (answer.isTrue && ch.IsChecked == answer.isTrue)
                        {
                            double m = (double)test.questions[i].mark /
                                (double)test.questions[i].numTrueAnswer;
                            count += m;
                        }
                    }
                    else
                    {
                        RadioButton rd = testViewers[i].listAnswers.Items[j] as RadioButton;



                        string a = (rd.Content as Label).Content.ToString();
                        passedAnswer.title = a;

                        if(rd.IsChecked == true)
                            passedTest.passedAnswers.Add(passedAnswer);

                        if (answer.isTrue && rd.IsChecked == answer.isTrue)
                        {
                            count += test.questions[i].mark;
                        }
                    }
                }
            }
            passedTest.result = count;
            user.passedTests.Add(passedTest);
            user = Client.updateUser(user, user.id_user);
            MainWindow.updateUserGroup();
            MainWindow.updateTest();
            MainWindow.updatePassedTest();
            MessageBox.Show("Вы небрали " + count + " баллов из " + countIsTrue);
        }
    }
}
