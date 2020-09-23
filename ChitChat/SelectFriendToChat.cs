using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChitChat
{
    public partial class SelectFriendToChat : Form
    {
        private User user { get; set; }

        private Listener listener { get; set; }

        #region ctors
        public SelectFriendToChat()
        {
            InitializeComponent();
        }
        public SelectFriendToChat(User user)
        {
            InitializeComponent();
            this.user = user;
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
            this.listener = new Listener();
            listener.OnStartAccessor(new string[] { });
        }


        protected override void OnClosed(EventArgs e)
        {
            listener?.OnStopAccessor();
            base.OnClosed(e);
        }
    }
}
