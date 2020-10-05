using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
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

        public static List<string> ongoingConversations { get; set; }

        public static List<Tuple<IPEndPoint,Message>> notifications { get; set; }

        #region ctors
        public UserMain()
        {
            InitializeComponent();
        }
        public UserMain(User user)
        {
            InitializeComponent();

            UserMain.user_ = user;
            //UserMain.contactsUsernames = new List<string>();
            UserMain.ongoingConversations = new List<string>();


            bs_ = new BindingSource();
            
        }

       
        #endregion


        private async void UserMain_Load(object sender, EventArgs e)
        {
            try
            {
                UserMain.user_ = await User.load_UserAsync(user_);
                this.welcomeLbl.Text += UserMain.user_.name_;
                this.Text = UserMain.user_.name_;
                this.MaximizeBox = false;


                if (UserMain.user_.ip_ == null || !Utilities.compareIPs(UserMain.user_.ip_.ToString()))
                    UserMain.user_.updateUserIPAsync(Utilities.GetLocalIPAddress());




                //await Task.Run(() => this.load_ContactList());

                UserMain.contactsUsernames = UserMain.user_.contacts_.Values.ToList();
                bs_.DataSource = UserMain.contactsUsernames;
                this.contacts.DataSource = bs_;
                if(contactsUsernames.Count != 0)this.contacts.SetSelected(0, true);

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

        
        

        /*private async Task load_ContactList()
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
        }*/

        protected override void OnClosed(EventArgs e)
        {
            
            
            listener_?.OnStopAccessor();
            this.listener_?.Dispose();

            UserMain.user_.contacts_.Clear();
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
            try
            {
                if (contacts.SelectedItem != null && !ongoingConversations.Contains(contacts.SelectedItem.ToString()))
                {
                    var chatting = new Chatting(new User(contacts.SelectedItem.ToString()));
                    chatting.Show();
                    this.Closed += (s, args) => chatting.Close();
                    UserMain.ongoingConversations.Add(contacts.SelectedItem.ToString());
                }
            }
            catch(Exception ex)
            {
                Logs logs = new Logs();
                logs.writeException(ex);
                MessageBox.Show(ex.Message);
            }
        }
    }
}
