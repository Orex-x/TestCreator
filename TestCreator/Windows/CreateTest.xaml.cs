using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using TestCreator.Objects;
using TestCreator.Service;
using TestCreator.TestCreatorAPI;

namespace TestCreator
{
    public partial class CreateTest : Window
    {
        public static ObservableCollection<QuestionFragment> listQustions = new ObservableCollection<QuestionFragment>();
        public bool answerIsChoose = false, savetest = true, updateTest = false;
        public long idTest;
        public int indexTest;
        private User _user;

        public static Dictionary<long, long> dicAnswerQuestion = new Dictionary<long, long>();

        public CreateTest() 
        {
            InitializeComponent();
            _user = StaticObject.mainUser;
            listQustions.Clear();
            ListViewQuestions.ItemsSource = listQustions;
            listQustions.Add(new QuestionFragment());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            listQustions.Add(new QuestionFragment());
        }

        private void Button_Click_DeleteQuestion(object sender, RoutedEventArgs e)
        {
            if (ListViewQuestions.SelectedItem != null)
            {
                listQustions.Remove(ListViewQuestions.SelectedItem as QuestionFragment);
            }
        }
        public void loadTest(Test test)
        {
            updateTest = true;
            idTest = test.idTest;
            indexTest = MainWindow.tests.IndexOf(test);
            listQustions.Clear();
            ThemeTest.Text = test.title;
            foreach (var questions in test.questions)
            {
                var qf = new QuestionFragment();
              
                qf.titleQuestion.Text = questions.title;

                if (questions.isCheckBox)
                {
                    qf.CheckBoxQuizMode.IsChecked = false;
                    qf.updateLists();
                    for (int i = 0; i < questions.answers.Count; i++)
                    {
                        TextBox textBox = new TextBox();
                        textBox.Text = questions.answers[i].title;
                        CheckBox ch = CreateElementUI.getCheckBox(20, new Thickness(0, 0, 0, 0),
                            questions.answers[i].isTrue, textBox,
                            HorizontalAlignment.Left, VerticalAlignment.Top);
                        qf.listCheckBox.Add(ch);
                    }
                }
                else
                {
                    qf.CheckBoxQuizMode.IsChecked = true;
                    qf.updateLists();
                    for (int i = 0; i < questions.answers.Count; i++)
                    {
                        var textBox = new TextBox();
                        textBox.Text = questions.answers[i].title;

                        var rb = CreateElementUI.getRadioButton(20, new Thickness(0, 0, 0, 0),
                            questions.answers[i].isTrue, textBox, 
                            questions.answers[i].groupName, 
                            HorizontalAlignment.Left, VerticalAlignment.Top);

                        qf.listRadioButton.Add(rb);
                    }
                }
                listQustions.Add(qf);
            }

        }

        private void Button_Click_Save_Test(object sender, RoutedEventArgs e)
        {
            if (listQustions.Count != 0)
            {
                savetest = true;
                Test test = new Test();
                test.questions = new ObservableCollection<Question>();
                if (ThemeTest.Text.Length > 0)
                {
                    test.title = ThemeTest.Text;
                    //проходимся по списку вопросов
                    for (int i = 0; i < listQustions.Count; i++)
                    {
                        var qf = listQustions[i];
                        if (qf.titleQuestion.Text.Length > 0)
                        {
                            var question = new Question();
                            int numTrueAnswer = 0;
                            answerIsChoose = false;
                            question.title = qf.titleQuestion.Text;
                            question.answers = new ObservableCollection<Answer>();
                            if (qf.listAnswers.Items.Count == 0)
                            {

                            }
                            else if (qf.listAnswers.Items.Count > 1)
                            {
                                if (qf.TextBoxMark.Text.Length > 0)
                                {
                                    try
                                    {
                                        int mark = Convert.ToInt32(qf.TextBoxMark.Text);
                                        question.mark = mark;
                                    }
                                    catch (Exception ee)
                                    {
                                        qf.labelError.Content = "Mark not number";
                                        savetest = false;
                                    }

                                    //какой обьект будет использоваться
                                    if (qf.CheckBoxQuizMode.IsChecked == true)
                                    {
                                        question.isCheckBox = false;
                                        //проходимся по списку ответов
                                        for (int j = 0; j < qf.listAnswers.Items.Count; j++)
                                        {
                                            RadioButton rb = qf.listAnswers.Items[j] as RadioButton;
                                            Answer answer = new Answer();
                                            if ((rb.Content as TextBox).Text.Length > 0)
                                            {
                                                answer.title = (rb.Content as TextBox).Text;
                                                answer.isTrue = rb.IsChecked.Value;
                                                answer.groupName = rb.GroupName;
                                                if (rb.IsChecked == true)
                                                {
                                                    numTrueAnswer++;
                                                    answerIsChoose = true;
                                                }
                                                question.answers.Add(answer);
                                            }
                                            else
                                            {
                                                savetest = false;
                                                qf.labelError.Content = "Answer titile is null";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        question.isCheckBox = true;
                                        //проходимся по списку ответов
                                        for (int j = 0; j < qf.listAnswers.Items.Count; j++)
                                        {
                                            CheckBox ch = qf.listAnswers.Items[j] as CheckBox;
                                            Answer answer = new Answer();
                                            if ((ch.Content as TextBox).Text.Length > 0)
                                            {
                                                answer.title = (ch.Content as TextBox).Text;
                                                answer.isTrue = ch.IsChecked.Value;
                                                if (ch.IsChecked == true)
                                                {
                                                    numTrueAnswer++;
                                                    answerIsChoose = true;
                                                }
                                                question.answers.Add(answer);
                                            }
                                            else
                                            {
                                                savetest = false;
                                                qf.labelError.Content = "Answer titile is null";
                                            }
                                        }
                                    }
                                    question.numTrueAnswer = numTrueAnswer;
                                    if (answerIsChoose)
                                    {
                                        test.questions.Add(question);
                                    }
                                    else
                                    {
                                        qf.labelError.Content = "Choose true answer";
                                        savetest = false;
                                    }
                                }
                                else
                                {
                                    qf.labelError.Content = "Mark is null";
                                    savetest = false;
                                }
                            }
                            else
                            {
                                qf.labelError.Content = "Answers is null";
                                savetest = false;
                            }
                        }
                        else
                        {
                            qf.labelError.Content = "Titile question is null";
                            savetest = false;
                        }
                    }
                    if (savetest)
                    {

                        if (updateTest)
                        {
                            Client.updateTest(test, idTest);
                            MainWindow.tests.RemoveAt(indexTest);
                            MainWindow.tests.Insert(indexTest, test);
                            MessageBox.Show("Test is update");
                        }
                        else
                        {
                            _user.tests.Add(test);
                            _user = Client.updateUser(_user, _user.id_user);
                            test.idTest = _user.tests[_user.tests.Count - 1].idTest;
                            MainWindow.tests.Add(test);
                            MainWindow.updateUserGroup();
                            MainWindow.updateTest();
                            MessageBox.Show("Test is save");
                        }

                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Titile test is null");
                    savetest = false;
                }
            }else
                MessageBox.Show("The test has no questions");
        }   
    }
}
