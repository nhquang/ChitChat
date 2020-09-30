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
    public partial class UserMain : Form
    {
        public static User user_ { get; set; }

        private Listener listener_ { get; set; }

        private List<string> contactsUsernames { get; set; }

        Task updateNoti_ = null;

        public CancellationTokenSource cts_ { get; set; }

        #region ctors
        public UserMain()
        {
            InitializeComponent();
        }
        public UserMain(User user)
        {
            InitializeComponent();

            UserMain.user_ = user;
            contactsUsernames = new List<string>();
        }
        #endregion


        private async void UserMain_Load(object sender, EventArgs e)
        {
            try
            {
                UserMain.user_ = await User.load_UserAsync(user_);
                this.welcomeLbl.Text += " " + user_.name_;

                

                if (UserMain.user_.ip_ == null || !Utilities.compareIPs(UserMain.user_.ip_.ToString()))
                    UserMain.user_.updateUserIPAsync(Utilities.GetLocalIPAddress());


                await Task.Run(() => this.load_ContactList());

                this.contacts.DataSource = contactsUsernames;

                this.listener_ = new Listener();
                listener_.OnStartAccessor(new string[] { });
                cts_ = new CancellationTokenSource();
                this.updateNoti_ = new Task(() => this.updateNotification_(cts_.Token), cts_.Token, TaskCreationOptions.LongRunning);
                this.updateNoti_.Start();
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
            while (!token.IsCancellationRequested)
            {
                //this.contacts.BeginInvoke((Action)(() => this.updateContactList()));
                

            }
        }

        private async Task load_ContactList()
        {
            User user = null;
            using (var database = new Database())
            {
                foreach (var item in UserMain.user_.contactIDs_)
                {
                    user = await database.selectUserByIDAsync(item);
                    this.contactsUsernames.Add(user.username_);
                }
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            
            cts_.Cancel();
            this.updateNoti_.Wait();
            this.updateNoti_.Dispose();
            listener_?.OnStopAccessor();
            this.listener_?.Dispose();

            UserMain.user_ = null;
            
           

            this.Dispose();
            base.OnClosed(e);
        }

        private void addCtBtn_Click(object sender, EventArgs e)
        {
            var addCon = new AddContact();
            addCon.Show();
            this.Closed += (s, args) => addCon.Close();
            addCon.FormClosed += AddCon_FormClosed;
        }

        private async void AddCon_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
    }
}
