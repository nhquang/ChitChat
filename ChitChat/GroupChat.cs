using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChitChat
{
    public partial class GroupChat : Form
    {
        List<string> membersList { get; set; }

        long id { get; set; }

        public static BindingSource groupMemberSource { get; set; }

        public GroupChat(long id, List<string> members)
        {
            this.id = id;
            InitializeComponent();
            membersList = members;
            groupMemberSource = new BindingSource();
        }
        private void GroupChat_Load(object sender, System.EventArgs e)
        {
            //welcomeLbl.Text += $"{ membersList[0] }, { membersList[1] }, ...";
            welcomeLbl.Text = "You're in a group chat.";
            groupMemberSource.DataSource = membersList;
            members.DataSource = groupMemberSource;
            displayMessages.Tick += DisplayMessages_Tick;
            displayMessages.Start();
        }

        private void DisplayMessages_Tick(object sender, EventArgs e)
        {
            try
            {
                for(int i = 0; i < UserMain.groupMessagesToBeDisplayed.Count; i++)
                {
                    if (UserMain.groupMessagesToBeDisplayed[i].groupID == this.id)
                    {
                        if (!UserMain.groupMessagesToBeDisplayed[i].accepted && !UserMain.groupMessagesToBeDisplayed[i].leave)
                            content.Text += $"{UserMain.groupMessagesToBeDisplayed[i].sender}: {UserMain.groupMessagesToBeDisplayed[i].message.Trim()}\n";

                        else if(UserMain.groupMessagesToBeDisplayed[i].accepted)
                        {
                            int temp = UserMain.groupMessagesToBeDisplayed[i].members.Count - this.membersList.Count;
                            for(int j = UserMain.groupMessagesToBeDisplayed[i].members.Count - 1 - temp + 1; temp > 0; temp--) {
                                this.membersList.Add(UserMain.groupMessagesToBeDisplayed[i].members[j]);
                                j--;
                            }
                            groupMemberSource.ResetBindings(false);
                        }
                        else if (UserMain.groupMessagesToBeDisplayed[i].leave)
                        {
                            this.membersList.Remove(UserMain.groupMessagesToBeDisplayed[i].sender);
                            groupMemberSource.ResetBindings(false);
                        }
                        UserMain.groupMessagesToBeDisplayed.RemoveAt(i);
                    }
                }
            }
            catch(Exception ex)
            {
                Logs logs = new Logs();
                logs.writeException(ex);
            }
        }

        protected override async void OnClosed(EventArgs e)
        {
            displayMessages.Stop();
            using (var database = new Database())
            {
                await database.openDatabaseAsync();
                foreach (var item in this.membersList)
                {
                    if (!item.Equals(UserMain.user_.username_))
                    {
                        var temp = new Message(UserMain.user_.username_, item, string.Empty, false, false, true, this.id);
                        var ip = await database.selectUsersDataByUsernameAsync(new User(item), Type.ip);
                        Listener.outgoingMessages.TryAdd(new Tuple<System.Net.IPEndPoint, Message>(new IPEndPoint(IPAddress.Parse((string)ip), Convert.ToInt16(ConfigurationSettings.AppSettings["port"].Trim())), temp));
                    }
                }
            }
            UserMain.ongoingGroupConversations.Remove(this);
            base.OnClosed(e);
        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch(Exception ex)
            {

            }
        }
    }
}
