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
using Newtonsoft.Json;

namespace ChitChat
{
    partial class Listener : ServiceBase
    {
        private UdpClient udpClient_ { get; set; }
        private Task receivingWorker_ { get; set; }
        private Task sendingWorker_ { get; set; }
        private CancellationTokenSource cts_ { get; set; }

        

        public static BlockingCollection<Tuple<IPEndPoint,Message>> incomingMessages { get; set; }
        public static BlockingCollection<Tuple<IPEndPoint,Message>> outgoingMessages { get; set; }

        public Listener()
        {
            InitializeComponent();
            cts_ = new CancellationTokenSource();
            Listener.incomingMessages = new BlockingCollection<Tuple<IPEndPoint, Message>>();
            Listener.outgoingMessages = new BlockingCollection<Tuple<IPEndPoint, Message>>();
        }
        public void OnStartAccessor(string[] args) => this.OnStart(args);
        public void OnStopAccessor() => this.OnStop();

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
            try
            {
                udpClient_ = new UdpClient(Convert.ToInt16(ConfigurationSettings.AppSettings["port"].Trim()));
         


                receivingWorker_ = new Task(() => this.listening(cts_.Token), cts_.Token, TaskCreationOptions.LongRunning);
                receivingWorker_.Start();

                sendingWorker_ = new Task(() => this.sending(cts_.Token), cts_.Token, TaskCreationOptions.LongRunning);
                sendingWorker_.Start();

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
                cts_?.Cancel();
                sendingWorker_.Wait();
                receivingWorker_?.Dispose();

                sendingWorker_?.Dispose();
                udpClient_?.Close();
                udpClient_?.Dispose();
                cts_?.Dispose();

                incomingMessages?.Dispose();
                outgoingMessages?.Dispose();
                
                base.OnStop();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async void sending(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                try
                {
                    if(Listener.outgoingMessages.TryTake(out Tuple<IPEndPoint,Message> temp))
                    {
                        //byte[] bytes = Encoding.ASCII.GetBytes(temp.Item2);
                        //await this.udpClient_.SendAsync(bytes, bytes.Length, temp.Item1);
                        var bytes = prepareMessage(temp.Item2);
                        await this.udpClient_.SendAsync(bytes, bytes.Length, temp.Item1);

                    }
                }
                catch(Exception ex)
                {
                    Logs logs = new Logs();
                    logs.writeException(ex);
                }
            }
        }

        private async void listening(CancellationToken ct)
        {
            try
            {
                while (!ct.IsCancellationRequested)
                {
                    try
                    {
                        var received = await udpClient_.ReceiveAsync();
                        //incomingMessages.TryAdd(new Tuple<IPEndPoint, string>(received.RemoteEndPoint, Encoding.ASCII.GetString(received.Buffer)));
                        incomingMessages.TryAdd(new Tuple<IPEndPoint, Message>(received.RemoteEndPoint, parseMessage(received.Buffer)));
                    }
                    catch (ObjectDisposedException ex)
                    {
                        throw;
                    }
                    
                }
            }
            catch(ObjectDisposedException ex)
            {

            }
            catch(Exception ex)
            {
                Logs logs = new Logs();
                logs.writeException(ex);
            }
        }

        private byte[] prepareMessage(Message message)
        {
            var temp = JsonConvert.SerializeObject(message);
            return Encoding.ASCII.GetBytes(temp);
        }
        private Message parseMessage(byte[] bytes)
        {
            var temp = Encoding.ASCII.GetString(bytes);
            return JsonConvert.DeserializeObject<Message>(temp);
        }
    }
}
