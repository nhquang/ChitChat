using ChitChat;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Engine.Listener
{
    public class TCPIPListener
    {
        private TcpListener tcpListener_ { get; set; }
        private Task worker_ { get; set; }
        private CancellationTokenSource cts_ { get; set; }

        public TCPIPListener()
        {
            tcpListener_ = new TcpListener(IPAddress.Parse(ConfigurationSettings.AppSettings["localIP"].Trim()), Convert.ToUInt16(ConfigurationSettings.AppSettings["port"].Trim()));
            cts_ = new CancellationTokenSource();
        }
        public void OnStart()
        {
            try
            {
                tcpListener_?.Start();
                worker_ = new Task(() => this.listening(cts_.Token), cts_.Token, TaskCreationOptions.LongRunning);
            }
            catch(Exception ex)
            {
                this.OnStop();
                throw;
            }
        }
        private void listening(CancellationToken ct)
        {
            NetworkStream stream = null;
            TcpClient client = null;
            if (!ct.IsCancellationRequested)
            {
                try
                {
                    using (client = tcpListener_.AcceptTcpClient())
                    {
                        using (stream = client.GetStream())
                        {
                            var buffer = new byte[client.ReceiveBufferSize];
                            int bytes = stream.Read(buffer, 0, client.ReceiveBufferSize);
                            Engine.messageReceived.Add(Encoding.ASCII.GetString(buffer, 0, bytes));
                            stream.Close();
                        }
                        client.Close();
                    }
                }
                catch (Exception ex)
                {
                    client?.Close();
                    client?.Dispose();
                    stream?.Close();
                    stream?.Dispose();
                    Logs logs = new Logs();
                    logs.writeException(ex);
                }

            }
            else ct.ThrowIfCancellationRequested();
        }
        public void OnStop()
        {
            try
            {
                if (worker_?.Status == TaskStatus.Running)
                {
                    cts_.Cancel();
                    cts_.Dispose();
                }
                tcpListener_?.Stop();
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
