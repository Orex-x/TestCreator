using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace TestCreator
{
    class Question 
    {
        TextBox questionTitle;
        List<RadioButton> answersRadioButton;
        List<CheckBox> answersCheckBox;

        public TextBox QuestionTitle { get; set; }
        public List<CheckBox> AnswersCheckBox { get; set; }
        public List<RadioButton> AnswersRadioButton { get; set; }

    }
}
