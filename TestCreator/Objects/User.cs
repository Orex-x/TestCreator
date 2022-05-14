 using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace TestCreator.Objects
{
    public class User
    {
        public long id_user { get; set; }
        public string first_name { get; set; }
        public string surname { get; set; }
        public string last_name { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string activationCode { get; set; }

        public bool active { get; set; }

        public ObservableCollection<Test> tests { get; set; }

        public ObservableCollection<PassedTest> passedTests { get; set; }

    }
}
