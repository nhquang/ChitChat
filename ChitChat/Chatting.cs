using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChitChat
{
    public partial class Chatting : Form
    {
       
        private User user_ { get; set; }

        #region Ctors
        public Chatting()
        {
            InitializeComponent();
        }

        public Chatting(User user)
        {
            InitializeComponent();
            
            this.user_ = user;
            this.receiving.Tick += (sender, args) => this.displayNewMessage(sender, args);

        }
        #endregion


        private async void Chatting_Load(object sender, EventArgs e)
        {
            try
            {
                user_ = await User.load_UserAsync(user_);
                this.welcomeLbl.Text += user_.name_;
                this.receiving.Start();
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logs logs = new Logs();
                logs.writeException(ex);
                this.Close();
            }
        }


        void displayNewMessage(object sender, EventArgs args)
        {
            try
            {

                if (Listener.incomingMessages.TryTake(out string temp))
                    content.Text += temp + "\n";
            }
            catch (Exception ex)
            {
                Logs logs = new Logs();
                logs.writeException(ex);
            }
        }
        
        

        protected override void OnClosed(EventArgs e)
        {
            try
            {
                this.receiving.Stop();
                this.receiving.Dispose();
                base.OnClosed(e);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logs logs = new Logs();
                logs.writeException(ex);
            }
        }

        
    }
}
