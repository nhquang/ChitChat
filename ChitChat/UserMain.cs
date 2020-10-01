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
    public partial class UserMain : Form
    {
        public static User user_ { get; set; }

        private Listener listener_ { get; set; }

        public static BindingSource bs_ { get; set; }

        public static List<string> contactsUsernames { get; set; }

        

        #region ctors
        public UserMain()
        {
            InitializeComponent();
        }
        public UserMain(User user)
        {
            InitializeComponent();

            UserMain.user_ = user;
            UserMain.contactsUsernames = new List<string>();
            bs_ = new BindingSource();
            bs_.DataSource = UserMain.contactsUsernames;
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
                this.contacts.DataSource = bs_;
                

                this.listener_ = new Listener();
                listener_.OnStartAccessor(new string[] { });
                

                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logs logs = new Logs();
                logs.writeException(ex);
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
                    UserMain.contactsUsernames.Add(user.username_);
                }
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            
            
            listener_?.OnStopAccessor();
            this.listener_?.Dispose();

            UserMain.user_.contactIDs_.Clear();
            UserMain.user_ = null;

            UserMain.contactsUsernames.Clear();
            UserMain.contactsUsernames = null;

            this.Dispose();
            base.OnClosed(e);
        }

        private void addCtBtn_Click(object sender, EventArgs e)
        {
            var addCon = new AddContact();
            addCon.Show();
            this.Closed += (s, args) => addCon.Close();
        }

        private void chatBtn_Click(object sender, EventArgs e)
        {
            var chatting = new Chatting(new User(contacts.SelectedItem.ToString()));
            chatting.Show();
            this.Closed += (s, args) => chatting.Close();
        }
    }
}
