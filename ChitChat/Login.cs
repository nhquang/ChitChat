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
            this.AcceptButton = signInBtn;
            this.MaximizeBox = false;
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
            try
            {
                if (!string.IsNullOrEmpty(usr.Text) && !string.IsNullOrEmpty(pwd.Text))
                {
                    bool check = await Login.authenticationAsync(new Tuple<string, string>(usr.Text, pwd.Text));
                    if (check)
                    {
                        var user = new User(usr.Text);
                        UserMain userMain = new UserMain(user);
                        this.Hide();
                        userMain.Show();
                        userMain.Closed += (s, args) => this.Show();

                    }
                    else MessageBox.Show("Username or Password is incorrect!");
                }
            }
            catch(Exception ex)
            {
                Logs logs = new Logs();
                logs.writeException(ex);
                MessageBox.Show(ex.Message);
            }
        }
        private async static Task<bool> authenticationAsync(Tuple<string,string> credentials)
        {
            User user = new User(credentials.Item1);
            string temp = string.Empty;
            try
            {
                using (var database = new Database())
                {
                    object check = await database.selectUsersDataByUsernameAsync(user, Type.exists);
                    if (check != null && (bool)check)
                        if (Utilities.hashPassword(credentials.Item2).Equals(await database.selectUsersDataByUsernameAsync(user, Type.password)))
                            return true;
                }
            }
            catch(Exception ex)
            {
                throw;
            }
            return false;
        }
    }
}
