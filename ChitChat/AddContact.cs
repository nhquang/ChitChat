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
        private List<User> users_ { get; set; }


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
                    users_ = await database.selectAllUsersAsync();
                }
                this.usrnames.AutoCompleteCustomSource.AddRange(users_.Select(u => u.username_).Where(u => u.Equals(user_.username_) ==  false).ToArray());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logs logs = new Logs();
                logs.writeException(ex);
            }
        }

        private void addBtn_Click(object sender, EventArgs e)
        {

        }

        protected override void OnClosed(EventArgs e)
        {
            this.Dispose();
            base.OnClosed(e);
        }
    }
}
