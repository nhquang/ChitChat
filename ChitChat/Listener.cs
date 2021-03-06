﻿using System;
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

        

        public static BlockingCollection<Message> incomingMessages { get; set; }
        public static BlockingCollection<Tuple<IPEndPoint,Message>> outgoingMessages { get; set; }

        public Listener()
        {
            InitializeComponent();
            cts_ = new CancellationTokenSource();
            Listener.incomingMessages = new BlockingCollection<Message>();
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
         

                //Start receiving messages Task
                receivingWorker_ = new Task(() => this.receiving(cts_.Token), cts_.Token, TaskCreationOptions.LongRunning);
                receivingWorker_.Start();

                //Start sending messages Task
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
        //sending out messages
        private async void sending(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                try
                {
                    if(Listener.outgoingMessages.TryTake(out Tuple<IPEndPoint,Message> temp))
                    {
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
        //Waiting for incoming messages
        private async void receiving(CancellationToken ct)
        {
            try
            {
                while (!ct.IsCancellationRequested)
                {
                    try
                    {
                        var received = await udpClient_.ReceiveAsync();
                        incomingMessages.TryAdd(parseMessage(received.Buffer));
                    }
                    catch (ObjectDisposedException ex)
                    {
                        throw;
                    }
                    catch(Exception ex)
                    {
                        Logs logs = new Logs();
                        logs.writeException(ex);
                    }
                    
                }
            }
            catch(ObjectDisposedException ex)
            {

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
