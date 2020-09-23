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
            try
            {
                User.load_User(ref user);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logs logs = new Logs();
                logs.writeException(ex);
            }
            this.welcomeLbl.Text += " " + user.name_;
        }
        #endregion


        private void SelectFriendToChat_Load(object sender, EventArgs e)
        {
            this.listener_ = new Listener();
            listener_.OnStartAccessor(new string[] { });
            this.updateNoti_ = new Task(() => this.updateNotification_(cts_.Token), cts_.Token, TaskCreationOptions.LongRunning);
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
            listener_?.OnStopAccessor();
            base.OnClosed(e);
        }
    }
}
