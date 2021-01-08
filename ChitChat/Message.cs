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
        public bool invitation { get; set; }
        public List<string> members { get; set; }
        public Message(string sender, string receiver, string message, bool invitation, List<string> members = null)
        {
            this.sender = sender;
            this.receiver = receiver;
            this.message = message;
            this.invitation = invitation;
            this.members = members;
        }
    }
}
