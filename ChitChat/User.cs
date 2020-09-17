using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitChat
{
    public class User
    {
        public string name_ { get; private set; }
        public string username_ { get; private set; }
        public string pass_ { get; private set; }
        public int age_ { get; private set; }
        public bool male_ { get; private set; }
        public string note_ { get; private set; }
        public User(string name, string username, string pass, int age, bool male, string note)
        {
            name_ = name;
            username_ = username;
            pass_ = pass;
            age_ = age;
            male_ = male;
            note_ = note;
        }
        public User()
        {
            
        }
         
    }
}
