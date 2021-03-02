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
    public partial class Invite : Form
    {
        List<string> selected_ { get; set; }
        private BindingSource bs_ { get; set; }
        List<string> currentMembersList_ { get; set; }
        long id_ { get; set; }
        public Invite(List<string> currentMembersList, long id)
        {
            this.id_ = id;
            this.currentMembersList_ = currentMembersList;
            InitializeComponent();
        }
        private async void Invite_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.AcceptButton = this.sendInvi;
            selected_ = new List<string>();
            bs_ = new BindingSource();
            bs_.DataSource = selected_;
            toBeInvited.DataSource = bs_;
            try
            {
                this.usrname.AutoCompleteCustomSource.AddRange(UserMain.user_.contacts_.Values.ToArray());
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logs logs = new Logs();
                logs.writeException(ex);
            }
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(usrname.Text) && !string.IsNullOrWhiteSpace(usrname.Text) && UserMain.user_.contacts_.ContainsValue(usrname.Text))
            {
                selected_.Add(usrname.Text);
                this.bs_.ResetBindings(false);
            }
        }

        private void rmBtn_Click(object sender, EventArgs e)
        {
            if (this.toBeInvited.SelectedItem != null)
            {
                selected_.Remove(usrname.Text);
                bs_.ResetBindings(false);
            }
        }
        protected override void OnClosed(EventArgs e)
        {

            this.bs_.Dispose();
            base.OnClosed(e);
        }

        private async void sendInvi_Click(object sender, EventArgs e)
        {
            try
            {
                using (var database = new Database())
                {
                    await database.openDatabaseAsync();
                    foreach (var item in selected_)
                    {
                        var ip = await database.selectUsersDataByUsernameAsync(new User(item), Type.ip);
                        var msg = new Message(UserMain.user_.username_, item, $"{ UserMain.user_.username_} wants to invite you to a group chat!!!", true, false, false, this.id_, this.currentMembersList_);
                        Listener.outgoingMessages.TryAdd(new Tuple<IPEndPoint, Message>(new IPEndPoint(IPAddress.Parse((string)ip), Convert.ToInt16(ConfigurationSettings.AppSettings["port"].Trim())), msg));
                    }
                }
                MessageBox.Show("Invitation(s) are sent successfully!!!");
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logs logs = new Logs();
                logs.writeException(ex);
            }
        }
    }
}
