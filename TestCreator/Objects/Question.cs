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
        public long id_question { get; set; }
        public string title { set; get; }
        public bool isCheckBox { set; get; }
        public int mark { set; get; }
        public int numTrueAnswer { set; get; }

        public ObservableCollection<Answer> answers { get; set; }

        public ObservableCollection<Test> tests { get; set; }
    }
}
