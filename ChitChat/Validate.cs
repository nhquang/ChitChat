using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChitChat
{
    public static class Validation
    {
        public static bool onlyLettersVal(string name)
        {
            Regex pattern = new Regex(@"^[a-zA-Z]+$");
            return pattern.IsMatch(name);

        }
        public static bool numOnlyVal(string num)
        {
            Regex pattern = new Regex(@"^[0-9][0-9]$");
            return pattern.IsMatch(num);
        }

        public static bool LettersAndNum(string val)
        {
            Regex pattern = new Regex(@"^[0-9a-zA-Z]+$");
            return pattern.IsMatch(val);
        }
    }
}
