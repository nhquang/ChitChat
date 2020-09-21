using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChitChat
{
    partial class Listener : ServiceBase
    {
        private UdpClient udpClient_ { get; set; }
        //private TcpListener tcpListener_ { get; set; }
        private Task worker_ { get; set; }
        private CancellationTokenSource cts_ { get; set; }
        private IPEndPoint remoteEP { get; set; }

        public static BlockingCollection<string> messages = new BlockingCollection<string>();

        public Listener()
        {
            InitializeComponent();
            //tcpListener_ = new TcpListener(IPAddress.Parse(ConfigurationSettings.AppSettings["localIP"].Trim()), Convert.ToUInt16(ConfigurationSettings.AppSettings["port"].Trim()));
            cts_ = new CancellationTokenSource();
            //remoteEP = new IPEndPoint(IPAddress.Any, Convert.ToInt16(ConfigurationSettings.AppSettings["port"].Trim()));
        }
        public void OnStartAccessor(string[] args) => this.OnStart(args);
        public void OnStopAccessor() => this.OnStop();

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
            try
            {
                udpClient_ = new UdpClient(Convert.ToInt16(ConfigurationSettings.AppSettings["port"].Trim()));

                worker_ = new Task(() => this.listening(cts_.Token), cts_.Token, TaskCreationOptions.LongRunning);
                worker_.Start();
                base.OnStart(args);
            }
            catch (Exception ex)
            {
                this.OnStop();
                
                throw;
            }
        }



        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
            try
            {
                if (worker_?.Status == TaskStatus.Running || worker_?.Status == TaskStatus.RanToCompletion)
                {
                    //worker_.Wait(cts_.Token);
                    cts_.Cancel();
                    var check = cts_.IsCancellationRequested;
                    //worker_.Wait();
                    worker_.Dispose();
                }
                udpClient_?.Close();
                udpClient_?.Dispose();
                cts_.Dispose();
                base.OnStop();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        private async void listening(CancellationToken ct)
        {
            
            try
            {
                while (true)
                {
                    if (ct.IsCancellationRequested) ct.ThrowIfCancellationRequested();
                    var received = await udpClient_.ReceiveAsync();
                    messages.Add(Encoding.ASCII.GetString(received.Buffer));
                    //var received = udpClient_.ReceiveAsync();
                    //messages.Add(Encoding.ASCII.GetString(received.Result.Buffer));
                    //received.Dispose();
                }
            }           
            catch(ObjectDisposedException ex)
            {

            }
            catch (Exception ex)
            {
                Logs logs = new Logs();
                logs.writeException(ex);
            }
        }
    }
}
