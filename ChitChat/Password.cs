using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitChat
{
    public static class Password
    {
        static public string hashPassword(string input)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(input);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            return System.Text.Encoding.ASCII.GetString(data);
        }
        public static string encryption(string pwd)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(pwd);
            return Convert.ToBase64String(bytes);
        }
        public static string decryption(string encrypted)
        {
            byte[] bytes = Convert.FromBase64String(encrypted);
            return Encoding.ASCII.GetString(bytes);
        }
    }
}
