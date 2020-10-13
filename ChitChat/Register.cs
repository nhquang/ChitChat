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
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void Register_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
        }

        protected override void OnClosed(EventArgs e)
        {
            this.Dispose();
            base.OnClosed(e);
        }

        private async void regBtn_Click(object sender, EventArgs e)
        {
            bool pass = false;
            try
            {
                #region Validation
                if (Validation.onlyLettersVal(name.Text))
                {
                    if (Validation.LettersAndNum(username.Text) && username.Text.Length >= 8)
                    {
                        if (Validation.LettersAndNum(pwd.Text) && pwd.Text.Length >= 8)
                        {
                            using (var database = new Database())
                            {
                                await database.openDatabaseAsync();
                                User user = new User(username.Text);
                                object check = await database.selectUsersDataByUsernameAsync(user, Type.exists);
                                if (check!= null && !(bool)check) pass = true;
                                else MessageBox.Show("Username already exists.");
                            }
                        }
                        else MessageBox.Show("Password can only contain letters and numbers, and has at least 8 characters.");
                    }
                    else MessageBox.Show("Username can only contain letters and numbers, and has at least 8 characters.");
                }
                else MessageBox.Show("Name can contain letters only.");
                #endregion



                if (pass)
                {

                    User user = new User(name.Text, username.Text, Utilities.hashPassword(pwd.Text), null, male.Checked, notes.Text, Utilities.GetLocalIPAddress());

                    try
                    {
                        using (var database = new Database())
                        {
                            await database.openDatabaseAsync();
                            await database.addUserAsync(user);
                        }
                        MessageBox.Show("You have successfully registered!");
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message);
                        //var logs = new Logs();
                        //logs.writeException(ex);
                        throw;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                var logs = new Logs();
                logs.writeException(ex);
            }
        }
    }
}
