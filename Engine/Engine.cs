using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Engine.Listener;
using ChitChat;

namespace Engine
{
    public partial class Engine : ServiceBase
    {
        public static BlockingCollection<object> messageReceived = new BlockingCollection<object>();

        private TCPIPListener listener_  { get; set; }

        public Engine()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                listener_ = new TCPIPListener();
                listener_.OnStart();
            }
            catch(Exception ex)
            {
                this.OnStop();
                Logs logs = new Logs();
                logs.writeException(ex);
            }

        }

        protected override void OnStop()
        {
            try
            {
                listener_?.OnStart();
            }
            catch (Exception ex)
            {
                Logs logs = new Logs();
                logs.writeException(ex);
            }
        }
    }
}
