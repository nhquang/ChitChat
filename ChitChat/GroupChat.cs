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
                        if (!UserMain.groupMessagesToBeDisplayed[i].accepted)
                        {
                            content.Text += $"{UserMain.groupMessagesToBeDisplayed[i].sender}: {UserMain.groupMessagesToBeDisplayed[i].message.Trim()}\n";
                            UserMain.groupMessagesToBeDisplayed.RemoveAt(i);
                        }
                        else
                        {
                            this.membersList = UserMain.groupMessagesToBeDisplayed[i].members;
                            groupMemberSource.ResetBindings(false);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Logs logs = new Logs();
                logs.writeException(ex);
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            displayMessages.Stop();
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
