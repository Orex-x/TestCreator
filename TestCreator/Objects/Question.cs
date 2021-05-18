using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Controls;
using TestCreator.Objects;

namespace TestCreator
{
    public class Question
    {
        string title;
        List<Answer> answers;
        bool isCheckBox;

        public string Title { set; get; }
        public List<Answer> Answers { set; get; }
        public bool IsCheckBox { set; get; }
    }
}
