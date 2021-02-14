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
using System.Configuration;
using System.Collections.Concurrent;

namespace ChitChat
{
    public partial class UserMain : Form
    {
        public static User user_ { get; set; }

        private Listener listener_ { get; set; }

        public static BindingSource bs { get; set; }

        public static List<string> contactsUsernames { get; set; }

        public static List<GroupChat> ongoingGroupConversations { get; set; }

        public static Dictionary<string, Chatting> ongoingConversations { get; set; }

        public static List<Message> messagesToBeDisplayed { get; set; }

        public static List<Message> groupMessagesToBeDisplayed { get; set; }

        public static List<Message> reservedMessages { get; set; }

        public static ConcurrentQueue<Message> acceptedInvitations { get; set; }

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

            UserMain.ongoingGroupConversations = new List<GroupChat>();

            UserMain.notifications = new Queue<Message>();

            UserMain.reservedMessages = new List<Message>();            //unread messages

            UserMain.messagesToBeDisplayed = new List<Message>();

            UserMain.groupMessagesToBeDisplayed = new List<Message>();

            UserMain.acceptedInvitations = new ConcurrentQueue<Message>();

            bs = new BindingSource();

            organizeMessages_.Tick += OrganizeMessages__Tick;

            checkNoti_.Tick += CheckNoti__Tick;

            this.checkAcceptedInvitation_.Tick += checkAcceptedInvitation__Tick;
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
                bs.DataSource = UserMain.contactsUsernames;
                this.contacts.DataSource = bs;
                if(contactsUsernames.Count != 0)this.contacts.SetSelected(0, true);

                this.listener_ = new Listener();
                listener_.OnStartAccessor(new string[] { });

                organizeMessages_.Start();
                checkNoti_.Start();
                checkAcceptedInvitation_.Start();
                
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
                    if (!newMessage.invitation && UserMain.ongoingConversations.ContainsKey(newMessage.sender)) UserMain.messagesToBeDisplayed.Add(newMessage);
                    else if (!newMessage.invitation)
                    {
                        if (!newMessage.groupID.HasValue)
                        {
                            reservedMessages.Add(newMessage);
                            notifications.Enqueue(newMessage);
                        }
                        else
                        {
                            groupMessagesToBeDisplayed.Add(newMessage);
                        }
                    }
                    else
                    {
                        Task.Run(() => {
                            DialogResult dialogResult = MessageBox.Show(newMessage.message, "Invitation", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes) acceptedInvitations.Enqueue(newMessage);
                        });
                        
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

        private async void checkAcceptedInvitation__Tick(object sender, EventArgs e)
        {
            Message message = null;
            try
            {
                if(acceptedInvitations.Count > 0)
                {
                    acceptedInvitations.TryDequeue(out message);
                    message.members.Add(UserMain.user_.username_);
                    IPAddress recipentIP = null;
                    using(var database = new Database())
                    {
                        await database.openDatabaseAsync();
                        var retrievedIP = (string) await database.selectUsersDataByUsernameAsync(new User(message.sender), Type.ip);
                        recipentIP = IPAddress.Parse(retrievedIP);
                    }
                    Listener.outgoingMessages.TryAdd(new Tuple<IPEndPoint, Message>(new IPEndPoint(recipentIP, Convert.ToInt16(ConfigurationSettings.AppSettings["port"].Trim())), new Message(UserMain.user_.username_, message.sender, "", false, true, message.groupID, message.members)));
                    var groupChat = new GroupChat(message.groupID.Value, message.members);
                    ongoingGroupConversations.Add(groupChat);
                    groupChat.Show();
                }
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
            

            organizeMessages_.Stop();
            organizeMessages_.Dispose();

            checkNoti_.Stop();
            checkNoti_.Dispose();

            checkAcceptedInvitation_.Stop();
            checkAcceptedInvitation_.Dispose();

            for(int i = UserMain.ongoingConversations.Count - 1; i >= 0; i--)
            {
                UserMain.ongoingConversations.ElementAt(i).Value.Close();
            }

            for(int i = 0; i < UserMain.ongoingGroupConversations.Count; i++)
            {
                ongoingGroupConversations[i].Close();
            }

            listener_?.OnStopAccessor();
            this.listener_?.Dispose();

            UserMain.user_.contacts_.Clear();
            UserMain.user_ = null;

            UserMain.contactsUsernames.Clear();
            UserMain.contactsUsernames = null;

            UserMain.acceptedInvitations = null;

            UserMain.ongoingConversations.Clear();
            UserMain.ongoingConversations = null;

            UserMain.ongoingGroupConversations.Clear();
            UserMain.ongoingGroupConversations = null;

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
            addCon.ShowDialog();
            //this.Closed += (s, args) => addCon.Close();
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

        private async void createGrpChat_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (contacts.SelectedItem != null)
                {
                    var temp = contacts.SelectedItem.ToString();
                    var id = DateTime.UtcNow.Ticks;
                    var msg = new Message(UserMain.user_.username_, temp, $"{ UserMain.user_.username_} wants to invite you to a group chat!!!", true, false, id, new List<string>() { UserMain.user_.username_ });
                    var recipent = await User.load_UserAsync(new User(temp));
                    Listener.outgoingMessages.TryAdd(new Tuple<IPEndPoint, Message>(new IPEndPoint(recipent.ip_, Convert.ToInt16(ConfigurationSettings.AppSettings["port"].Trim())), msg));
                    var grpChat = new GroupChat(id, new List<string>() { UserMain.user_.username_ });
                    UserMain.ongoingGroupConversations.Add(grpChat);
                    grpChat.Show();
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
