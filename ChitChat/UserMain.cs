using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace ChitChat
{
    public partial class UserMain : Form
    {
        public static User user_ { get; set; }

        private Listener listener_ { get; set; }

        public static BindingSource bs_ { get; set; }

        public static List<string> contactsUsernames { get; set; }

        //public static List<string> ongoingConversations { get; set; }

        public static Dictionary<string, Chatting> ongoingConversations { get; set; }

        public static List<Message> messagesToBeDisplayed { get; set; } 

        public static List<Message> reservedMessages { get; set; }

        public static Queue<Message> notifications { get; set; }

        #region ctors
        public UserMain()
        {
            InitializeComponent();
        }
        public UserMain(User user)
        {
            InitializeComponent();

            UserMain.user_ = user;

            UserMain.ongoingConversations = new Dictionary<string, Chatting>();

            UserMain.notifications = new Queue<Message>();

            UserMain.reservedMessages = new List<Message>();            //unread messages

            UserMain.messagesToBeDisplayed = new List<Message>();

            bs_ = new BindingSource();

            organizeMessages_.Tick += OrganizeMessages__Tick;

            checkNoti_.Tick += CheckNoti__Tick;
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

                organizeMessages_.Start();
                checkNoti_.Start();

                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logs logs = new Logs();
                logs.writeException(ex);
            }
        }

        private void OrganizeMessages__Tick(object sender, EventArgs e)
        {
            try
            {
                if (Listener.incomingMessages.TryTake(out Message newMessage) && newMessage.receiver.Equals(UserMain.user_.username_))
                {
                    if (UserMain.ongoingConversations.ContainsKey(newMessage.sender)) UserMain.messagesToBeDisplayed.Add(newMessage);
                    else
                    {
                        reservedMessages.Add(newMessage);
                        notifications.Enqueue(newMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                Logs logs = new Logs();
                logs.writeException(ex);
            }
        }

        private void CheckNoti__Tick(object sender, EventArgs e)
        {
            Message temp = null;
            try
            {

                if (notifications.Count > 0)
                {
                    temp = notifications.Dequeue();
                    notificationBox.Text += "You got a new message from " + temp.sender + "\n";
                }

            }
            catch (Exception ex)
            {
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
            

            organizeMessages_.Stop();
            organizeMessages_.Dispose();

            checkNoti_.Stop();
            checkNoti_.Dispose();

            for(int i = UserMain.ongoingConversations.Count - 1; i >= 0; i--)
            {
                UserMain.ongoingConversations.ElementAt(i).Value.Close();
            }

            listener_?.OnStopAccessor();
            this.listener_?.Dispose();

            UserMain.user_.contacts_.Clear();
            UserMain.user_ = null;

            UserMain.contactsUsernames.Clear();
            UserMain.contactsUsernames = null;

            UserMain.ongoingConversations.Clear();
            UserMain.ongoingConversations = null;

            UserMain.notifications.Clear();
            UserMain.notifications = null;

            UserMain.reservedMessages.Clear();
            UserMain.reservedMessages = null;

            UserMain.messagesToBeDisplayed.Clear();
            UserMain.messagesToBeDisplayed = null;


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
                if (contacts.SelectedItem != null && !ongoingConversations.ContainsKey(contacts.SelectedItem.ToString()))
                {
                    var chatting = new Chatting(new User(contacts.SelectedItem.ToString()));
                    chatting.Show();
                    UserMain.ongoingConversations.Add(contacts.SelectedItem.ToString(), chatting);
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
