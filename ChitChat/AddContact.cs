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
    public partial class AddContact : Form
    {
        private List<User> allUsers_ { get; set; }


        #region Ctors
        public AddContact()
        {
            InitializeComponent();
        }
        #endregion

        private async void AddContact_Load(object sender, EventArgs e)
        {
            LayoutModifier.centerControlHorizontally(this.addBtn);
            try
            {
                using (var database = new Database())
                {
                    allUsers_ = await database.selectAllUsersAsync();
                }
                this.usrname.AutoCompleteCustomSource.AddRange(allUsers_.Select(u => u.username_).Where(u => u.Equals(UserMain.user_.username_) ==  false).ToArray());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logs logs = new Logs();
                logs.writeException(ex);
            }
        }

        private async void addBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (allUsers_.Select(u => u.username_).Contains(usrname.Text))
                {
                    var temp = await User.load_UserAsync(new User(usrname.Text));
                    if (!UserMain.user_.contactIDs_.Contains(temp.id_))
                    {

                        using (var database = new Database())
                        {
                            await database.addContactAsync(UserMain.user_, temp);
                        }
                        UserMain.user_.contactIDs_.Add(temp.id_);
                        UserMain.contactsUsernames.Add(temp.username_);
                        UserMain.bs_.ResetBindings(false);

                        MessageBox.Show("New contact has been added!");
                    }
                    else MessageBox.Show("This user is already in your contact list");
                }
                else MessageBox.Show("Username doesn't exist in the database");
            }
            catch(Exception ex)
            {
                Logs logs = new Logs();
                logs.writeException(ex);
                MessageBox.Show(ex.Message);
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            this.allUsers_?.Clear();
            this.allUsers_ = null;
            this.Dispose();
            base.OnClosed(e);
        }
    }
}
