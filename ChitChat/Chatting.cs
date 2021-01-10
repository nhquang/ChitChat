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
       
        private User chattingWith_ { get; set; }

        #region Ctors
        public Chatting()
        {
            InitializeComponent();
        }

        public Chatting(User user)
        {
            InitializeComponent();
            
            this.chattingWith_ = user;
            this.timer_.Tick += (sender, args) => this.displayNewMessage(sender, args);

        }
        #endregion


        private async void Chatting_Load(object sender, EventArgs e)
        {
            try
            {
                chattingWith_ = await User.load_UserAsync(chattingWith_);
                this.welcomeLbl.Text += chattingWith_.name_;
                this.Text = chattingWith_.username_;
                
                this.AcceptButton = sendBtn;
                this.MaximizeBox = false;
                //displayUnreadMessages();
                await Task.Run(() => this.displayUnreadMessages());
                this.timer_.Start();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logs logs = new Logs();
                logs.writeException(ex);
                this.Close();
            }
        }

        void displayUnreadMessages()
        {
            try
            {
                var temp = new List<int>();
                foreach (var message in UserMain.reservedMessages)
                {
                    int i = 0;
                    if (message.sender.Equals(this.chattingWith_.username_))
                    {
                        //this.content.Text += chattingWith_.username_ + ": " + message.message.Trim() + "\n";
                        this.content.Text += $"{chattingWith_.username_}: {message.message.Trim()}\n";
                        temp.Add(i);
                    }
                    i++;
                }
                foreach (var idx in temp)
                {
                    UserMain.reservedMessages.RemoveAt(idx);
                }
            }
            catch(Exception ex)
            {
                Logs logs = new Logs();
                logs.writeException(ex);
            }
        }

        void displayNewMessage(object sender, EventArgs args)
        {
            try
            {
                /*for(int i = 0; i < UserMain.messagesToBeDisplayed.Count; i++)
                {
                    if (UserMain.messagesToBeDisplayed[i].sender.Equals(this.chattingWith_.username_))
                    {
                        this.content.Text += chattingWith_.username_ + ": " + UserMain.messagesToBeDisplayed[i].message.Trim() + "\n";
                        UserMain.messagesToBeDisplayed.RemoveAt(i);
                    }
                }*/

                var temp = new List<int>();
                foreach (var message in UserMain.messagesToBeDisplayed)
                {
                    int i = 0;
                    if (message.sender.Equals(this.chattingWith_.username_))
                    {
                        this.content.Text += chattingWith_.username_ + ": " + message.message.Trim() + "\n";
                        temp.Add(i);
                    }
                    i++;
                }
                foreach (var idx in temp)
                {
                    UserMain.messagesToBeDisplayed.RemoveAt(idx);
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

                UserMain.ongoingConversations.Remove(this.chattingWith_.username_);

                this.Dispose();
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
                    var package = new Tuple<IPEndPoint, Message>(new IPEndPoint(this.chattingWith_.ip_, Convert.ToInt16(ConfigurationSettings.AppSettings["port"].Trim())), new Message(UserMain.user_.username_, this.chattingWith_.username_, send.Text.Trim('\n'), false, false));
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
