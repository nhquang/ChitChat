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
        private User user_ { get; set; }
        private List<User> allUsers_ { get; set; }


        #region Ctors
        public AddContact()
        {
            InitializeComponent();
        }
        public AddContact(User user)
        {
            InitializeComponent();
            user_ = user;
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
                this.usrname.AutoCompleteCustomSource.AddRange(allUsers_.Select(u => u.username_).Where(u => u.Equals(user_.username_) ==  false).ToArray());
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
                    if (!user_.contactIDs_.Contains(temp.id_))
                    {
                        //user_.contactIDs_.Add(temp.id_);
                        using (var database = new Database())
                        {
                            database.addContactAsync(user_, temp);
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
            this.Dispose();
            base.OnClosed(e);
        }
    }
}
