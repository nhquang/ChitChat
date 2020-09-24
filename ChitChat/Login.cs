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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        private void Login_Load(object sender, EventArgs e)
        {

        }
        protected override void OnClosed(EventArgs e)
        {
            this.Dispose();
            base.OnClosed(e);
        }

        private void regBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            var reg = new Register();
            reg.Show();
            reg.Closed += (s, args) => this.Show();
        }

        private async void signInBtn_Click(object sender, EventArgs e)
        {
            bool check = await Login.authenticationAsync(new Tuple<string, string>(usr.Text, pwd.Text));
            if (check)
            {
                SelectFriendToChat selectFriendToChat = new SelectFriendToChat(new User(usr.Text));
                this.Hide();
                selectFriendToChat.Show();
                selectFriendToChat.Closed += (s, args) => this.Show();

            }
            else MessageBox.Show("Username or Password is incorrect!");

        }
        private async static Task<bool> authenticationAsync(Tuple<string,string> credentials)
        {
            User user = new User(credentials.Item1);
            string temp = string.Empty;
            try
            {
                using (var database = new Database())
                {
                    bool check = await database.UserExistsAsync(user);
                    if (check)
                        if (Utilities.hashPassword(credentials.Item2).Equals(database.userPwd(user)))
                            return true;
                }
            }
            catch(Exception ex)
            {
                Logs logs = new Logs();
                logs.writeException(ex);
                MessageBox.Show(ex.Message);
            }
            return false;
        }
    }
}
