using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChitChat
{
    public partial class SelectFriendToChat : Form
    {
        private User user_ { get; set; }

        private Listener listener_ { get; set; }

        Task updateNoti_ = null;

        public CancellationTokenSource cts_ { get; set; }
        #region ctors
        public SelectFriendToChat()
        {
            InitializeComponent();
        }
        public SelectFriendToChat(User user)
        {
            InitializeComponent();
            this.user_ = user;
            
        }
        #endregion


        private async void SelectFriendToChat_Load(object sender, EventArgs e)
        {
            try
            {
                user_ = await User.load_UserAsync(user_);
                this.welcomeLbl.Text += " " + user_.name_;

                if (user_.ip_ == null || !Utilities.compareIPs(user_.ip_.ToString()))
                    user_.updateUserIPAsync(Utilities.GetLocalIPAddress());

                

                this.listener_ = new Listener();
                listener_.OnStartAccessor(new string[] { });
                cts_ = new CancellationTokenSource();
                this.updateNoti_ = new Task(() => this.updateNotification_(cts_.Token), cts_.Token, TaskCreationOptions.LongRunning);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logs logs = new Logs();
                logs.writeException(ex);
            }
        }

        private void updateNotification_(CancellationToken token)
        {
            while (true)
            {
                token.ThrowIfCancellationRequested();

            }
        }

        protected override void OnClosed(EventArgs e)
        {
            //this.updateNoti_.Wait();
            cts_.Cancel();          
            this.updateNoti_.Dispose();
            listener_?.OnStopAccessor();
            
            this.Dispose();
            base.OnClosed(e);
        }

        private void addCtBtn_Click(object sender, EventArgs e)
        {
            var addCon = new AddContact(user_);
            addCon.Show();
        }
    }
}
