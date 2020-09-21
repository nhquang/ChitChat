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
    public partial class Dashboard : Form
    {
        Listener listener = null;
        public Dashboard()
        {
            InitializeComponent();
        }
        private void Dashboard_Load(object sender, EventArgs e)
        {
            try
            {
                this.listener = new Listener();
                listener.OnStartAccessor(new string[] { });
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void updateContent()
        {
            
        }
        protected override void OnClosed(EventArgs e)
        {
            try
            {
                listener?.OnStopAccessor();
                base.OnClosed(e);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
