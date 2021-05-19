using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;
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
    public partial class CreateTest : Window
    {
        public static ObservableCollection<QuestionFragment> listQustions = new ObservableCollection<QuestionFragment>();
        public bool answerIsChoose = false, savetest = true;

        public CreateTest()
        {
            InitializeComponent();
            listQustions.Clear();
            ListViewQuestions.ItemsSource = listQustions;
            listQustions.Add(new QuestionFragment());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            listQustions.Add(new QuestionFragment());
        }

        public void loadTest(Test test)
        {
            listQustions.Clear();
            ThemeTest.Text = test.Title;
            foreach (Question questions in test.Questions){
                QuestionFragment qf = new QuestionFragment();
                
               /* TextBox box = CreateElementUI.getTextBox(100, 40, new Thickness(0, 0, 0, 0), questions.Title,
                    HorizontalAlignment.Left, VerticalAlignment.Top);
*/
                TextBox box = new TextBox();
                box.Text = questions.Title;

                qf.TitleQuestion = box;

                if (questions.IsCheckBox)
                {
                    qf.CheckBoxQuizMode.IsChecked = false;
                    qf.updateLists();
                    for (int i = 0; i < questions.Answers.Count; i++)
                    {
                        TextBox textBox = new TextBox();
                        textBox.Text = questions.Answers[i].Title;
                        CheckBox ch = CreateElementUI.getCheckBox(100, 20, new Thickness(0, 0, 0, 0), 
                            questions.Answers[i].IsTrue, textBox,
                            HorizontalAlignment.Left, VerticalAlignment.Top);
                        qf.listCheckBox.Add(ch);
                    }
                }
                else
                {
                    qf.CheckBoxQuizMode.IsChecked = true;
                    qf.updateLists();
                    for (int i = 0; i < questions.Answers.Count; i++)
                    {
                        TextBox textBox = new TextBox();
                        textBox.Text = questions.Answers[i].Title;

                        RadioButton rb = CreateElementUI.getRadioButton(100, 20, new Thickness(0, 0, 0, 0),
                            questions.Answers[i].IsTrue, textBox, questions.Answers[i].GroupName, HorizontalAlignment.Left, VerticalAlignment.Top);
                        qf.listRadioButton.Add(rb);
                    }
                }
                listQustions.Add(qf);
            }

        }

        private void Button_Click_Save_Test(object sender, RoutedEventArgs e)
        {
            savetest = true;
            Test test = new Test();
            test.Questions = new List<Question>();
            if (ThemeTest.Text.Length > 0)
            {
                test.Title = ThemeTest.Text;
                //проходимся по списку вопросов
                for (int i = 0; i < listQustions.Count; i++)
                {
                    QuestionFragment qf = listQustions[i];
                    if(qf.TitleQuestion.Text.Length > 0)
                    {
                        Question question = new Question();
                        int numTrueAnswer = 0;
                        answerIsChoose = false;
                        question.Title = qf.TitleQuestion.Text;
                        question.Answers = new List<Answer>();
                        if(qf.listAnswers.Items.Count > 1)
                        {
                            if (qf.TextBoxMark.Text.Length > 0)
                            {
                                try
                                {
                                    int mark = Convert.ToInt32(qf.TextBoxMark.Text);
                                    question.Mark = mark;
                                }catch(Exception ee)
                                {
                                    qf.labelError.Content = "Mark not number";
                                    savetest = false;
                                }
                           
                            //какой обьект будет использоваться
                            if (qf.CheckBoxQuizMode.IsChecked == true)
                            {
                                question.IsCheckBox = false;
                                //проходимся по списку ответов
                                for (int j = 0; j < qf.listAnswers.Items.Count; j++)
                                {
                                    RadioButton rb = qf.listAnswers.Items[j] as RadioButton;
                                    Answer answer = new Answer();
                                    if ((rb.Content as TextBox).Text.Length > 0)
                                    {
                                        answer.Title = (rb.Content as TextBox).Text;
                                        answer.IsTrue = rb.IsChecked.Value;
                                        answer.GroupName = rb.GroupName;
                                            if (rb.IsChecked == true)
                                            {
                                                numTrueAnswer++;
                                                answerIsChoose = true;
                                            }
                                        question.Answers.Add(answer);
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
                                question.IsCheckBox = true;
                                //проходимся по списку ответов
                                for (int j = 0; j < qf.listAnswers.Items.Count; j++)
                                {
                                    CheckBox ch = qf.listAnswers.Items[j] as CheckBox;
                                    Answer answer = new Answer();
                                    if ((ch.Content as TextBox).Text.Length > 0)
                                    {
                                        answer.Title = (ch.Content as TextBox).Text;
                                        answer.IsTrue = ch.IsChecked.Value;
                                            if (ch.IsChecked == true)
                                            {
                                                numTrueAnswer++;
                                                answerIsChoose = true;
                                            }
                                            question.Answers.Add(answer);
                                    }
                                    else
                                    {
                                        savetest = false;
                                        qf.labelError.Content = "Answer titile is null";
                                    }
                                }
                            }
                                question.NumTrueAnswer = numTrueAnswer;
                            if (answerIsChoose)
                            {
                                test.Questions.Add(question);
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
                    MainWindow.tests.Add(test);
                    MessageBox.Show("Test is save");
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Titile test is null");
                savetest = false;
            }
               
        }   

           
    }
}
