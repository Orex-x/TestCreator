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
        ObservableCollection<QuestionFragment> listQustions = new ObservableCollection<QuestionFragment>();
        List<string> listQuestionsJSON = new List<string>();
        public bool answerIsChoose = false, savetest = true;

        public CreateTest()
        {
            InitializeComponent();
            ListViewQuestions.ItemsSource = listQustions;
            listQustions.Add(new QuestionFragment());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            listQustions.Add(new QuestionFragment());
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
                        answerIsChoose = false;
                        question.Title = qf.TitleQuestion.Text;
                        question.Answers = new List<Answer>();
                        if(qf.listAnswers.Items.Count > 1)
                        {
                            //проходимся по списку ответов
                            for(int j = 0; j < qf.listAnswers.Items.Count; j++)
                            {
                                //какой обьект будет использоваться
                                if(qf.CheckBoxQuizMode.IsChecked == true)
                                {
                                    RadioButton rb = qf.listAnswers.Items[j] as RadioButton;
                                    Answer answer = new Answer();
                                    if ((rb.Content as TextBox).Text.Length > 0)
                                    {
                                        answer.Title = (rb.Content as TextBox).Text;
                                        answer.IsTrue = rb.IsChecked.Value;
                                        if(rb.IsChecked == true) answerIsChoose = true;
                                        question.Answers.Add(answer);
                                    }
                                    else
                                    {
                                        savetest = false;
                                        qf.labelError.Content = "Answer titile is null";
                                    }
                                }
                                else
                                {
                                    CheckBox ch = qf.listAnswers.Items[j] as CheckBox;
                                    Answer answer = new Answer();
                                    if ((ch.Content as TextBox).Text.Length > 0)
                                    {
                                        answer.Title = (ch.Content as TextBox).Text;
                                        answer.IsTrue = ch.IsChecked.Value;
                                        if (ch.IsChecked == true) answerIsChoose = true;
                                        question.Answers.Add(answer);
                                    }
                                    else
                                    {
                                        savetest = false;
                                        qf.labelError.Content = "Answer titile is null";
                                    }
                                        
                                }
                            }
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
                    //string json = JsonSerializer.Serialize<Test>(test);
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
