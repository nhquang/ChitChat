using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Listener
{
    public class TCPIPListener
    {
        private TcpListener tcpListener { get; set; }
        private BlockingCollection<object> messageReceived { get; set; }

    }
}
