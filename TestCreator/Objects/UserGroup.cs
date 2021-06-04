using System;
using System.Collections.Generic;
using System.Text;

namespace TestCreator.Objects
{
    public class UserGroup
    {
       public User user { get; set; }
       public Group group { get; set; }
 
       public bool is_admin { get; set; }
    }
}
