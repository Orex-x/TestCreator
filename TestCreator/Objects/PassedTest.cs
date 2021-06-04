using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace TestCreator.Objects
{
    public class PassedTest
    {
        public long id_passed_test { get; set; }
        public Test test { get; set; }
        public double result { get; set; }
        public ObservableCollection<PassedAnswer> passedAnswers { get; set; }

    }
}
