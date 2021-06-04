using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace TestCreator.Objects
{
    public class Answer
    {

        public long id_answer { get; set; }
        public string title { get; set; }
        public bool isTrue { get; set; }
        public string groupName { get; set; }

        public ObservableCollection<Question> questions { get; set; }
    }
}



