using System;
using System.Collections.Generic;
using System.Text;

namespace TestCreator.Objects
{
    public class PassedAnswer
    {
        public long id_passed_test_answer { get; set; }
        public Question question { get; set; }
        public string title { get; set; }
    }
}
