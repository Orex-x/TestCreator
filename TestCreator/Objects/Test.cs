using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace TestCreator.Objects
{
    public class Test
    {
        public long idTest { get; set; }
        public string title { set; get; }

        public ObservableCollection<Question> questions { get; set; }
    }
}
