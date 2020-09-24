using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChitChat
{
    public static class Utilities
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
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();
            return "";
        }
        public static bool compareIPs(string savedIP) { return savedIP.Equals(GetLocalIPAddress()); }
       
    }
}
