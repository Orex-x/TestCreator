using System;
using System.Collections.Generic;
using System.Text;

namespace TestCreator.Objects
{
    class User
    {
        public const string ROLE_USER = "uSer";
        public const string ROLE_ADMIN = "AdMiN";

        string role, login, password, name, surname, patronomyc; 



        public string Lole { set; get; }
        public string Login { set; get; }
        public string Password { set; get; }
        public string Name { set; get; }
        public string Surname { set; get; }
        public string Patronomyc { set; get; }
    }
}
