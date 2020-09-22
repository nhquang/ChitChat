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

        private void signInBtn_Click(object sender, EventArgs e)
        {

            if (Login.authentication(new Tuple<string, string>(usr.Text, pwd.Text)))
            {
                Dashboard dashboard = new Dashboard();
                this.Hide();
                dashboard.Show();
                dashboard.Closed += (s, args) => this.Show();
            }
            else MessageBox.Show("Username or Password is incorrect!");

        }
        private static bool authentication(Tuple<string,string> credentials)
        {
            User user = new User(credentials.Item1);
            string temp = string.Empty;
            try
            {
                using (var database = new Database())
                {
                    if (database.UserExists(user))
                        if (Password.hashPassword(credentials.Item2).Equals(database.userPwd(user)))
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
