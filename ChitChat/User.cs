using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public IPAddress ip_ { get; private set; }
        public List<User> contacts_ { get; private set; }

        public User(string name, string username, string pass, int? age, bool? male, string note, string ip)
        {
            name_ = name;
            username_ = username;
            pass_ = pass;
            if (age.HasValue) age_ = age.Value;
            else age_ = 0;
            if (male.HasValue) male_ = male.Value;
            note_ = note;
            if (ip != null) ip_ = IPAddress.Parse(ip);
            else ip_ = null;
        }
        public User(string username)
        {
            username_ = username;
        }
        public static void load_User(ref User user)
        {
            try
            {
                using (var database = new Database())
                {
                    database.selectUser(ref user);
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public void updateUserIP(string newIp)
        {
            this.ip_ = IPAddress.Parse(newIp);
        }
    }
}
