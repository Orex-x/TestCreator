using System;
using System.Collections.Generic;
using System.Text;

namespace TestCreator.Objects
{
    public class Test
    {
        string title;
        List<Question> questions;

        public string Title { set; get; }
        public List<Question> Questions { set; get; }
    }
}
