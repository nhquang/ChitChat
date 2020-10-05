using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitChat
{
    
    public class Message
    {
        public string sender { get; set; }
        public string receiver { get; set; }
        public string message { get; set; }
        public Message(string sender, string receiver, string message)
        {
            this.sender = sender;
            this.receiver = receiver;
            this.message = message;
        }
    }
}
