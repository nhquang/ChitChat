using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChitChat
{
    public partial class Chatting : Form
    {
       
        private User user_ { get; set; }

        #region Ctors
        public Chatting()
        {
            InitializeComponent();
        }

        public Chatting(User user)
        {
            InitializeComponent();
            
            this.user_ = user;
            this.timer_.Tick += (sender, args) => this.displayNewMessage(sender, args);

        }
        #endregion


        private async void Chatting_Load(object sender, EventArgs e)
        {
            try
            {
                user_ = await User.load_UserAsync(user_);
                this.welcomeLbl.Text += user_.name_;
                this.Text = user_.username_;
                this.timer_.Start();
                this.AcceptButton = sendBtn;
                this.MaximizeBox = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logs logs = new Logs();
                logs.writeException(ex);
                this.Close();
            }
        }

        

        void displayNewMessage(object sender, EventArgs args)
        {
            try
            {

                if (Listener.incomingMessages.TryTake(out Tuple<IPEndPoint,string> temp))
                {
                    if (temp.Item1.Address.Equals(UserMain.user_.ip_)) content.Text += this.user_.username_ + ": " + temp.Item2.Trim('\n') + "\n";
                    else Listener.incomingMessages.TryAdd(temp);
                }
            }
            catch (Exception ex)
            {
                Logs logs = new Logs();
                logs.writeException(ex);
            }
        }
        
        

        protected override void OnClosed(EventArgs e)
        {
            try
            {
                this.timer_.Stop();
                this.timer_.Dispose();

                UserMain.ongoingConversations.Remove(this.user_.username_);

                base.OnClosed(e);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logs logs = new Logs();
                logs.writeException(ex);
            }
        }

        

        private void sendBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(send.Text) || !string.IsNullOrEmpty(send.Text))
                {
                    var package = new Tuple<IPEndPoint, string>(new IPEndPoint(this.user_.ip_, Convert.ToInt16(ConfigurationSettings.AppSettings["port"].Trim())), send.Text);
                    Listener.outgoingMessages.TryAdd(package);
                    this.content.Text += "Me: " + send.Text.Trim('\n') + "\n";
                    this.send.Text = string.Empty;
                }
            }
            catch(Exception ex)
            {
                Logs logs = new Logs();
                logs.writeException(ex);
            }
        }
    }
}
