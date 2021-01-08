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

        public static BindingSource groupMemberSource { get; set; }

        public GroupChat(List<string> members)
        {
            InitializeComponent();
            membersList = members;
            
        }
        private void GroupChat_Load(object sender, System.EventArgs e)
        {
            welcomeLbl.Text += $"{ membersList[0] }, { membersList[1] }, ...";
            groupMemberSource.DataSource = membersList;
            members.DataSource = groupMemberSource;
        }
    }
}
