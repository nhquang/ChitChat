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
    public partial class Welcome : Form
    {
        public Welcome()
        {
            InitializeComponent();
        }

        private void Welcome_Load(object sender, EventArgs e)
        {
            LayoutModifier.centerControlHorizontally(signInBtn);
        }

        private void signInBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            var login = new Login();
            login.Show();
            login.Closed += (s, args) => this.Show();
        }
    }
}
