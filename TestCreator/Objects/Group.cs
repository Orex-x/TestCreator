using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace TestCreator.Objects
{
    public class Group
    {
        public struct Security
        {
            public static string Public = "public";
            public static string Private = "private";
        }
        
        public long id_group { get; set; }
        public string name { get; set; }
        
        public string password { get; set; }
        public string security_status { get; set; }
        public string invitationLink { get; set; }
        public ObservableCollection<Test> tests { get; set; }
    }
}
