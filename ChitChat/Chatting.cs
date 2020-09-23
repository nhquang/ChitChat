﻿using System;
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
        Task updateContent = null;
        CancellationTokenSource cancellationTokenSource = null;
        private User user { get; set; }


        #region Ctors
        public Chatting()
        {
            InitializeComponent();
        }

        public Chatting(User user)
        {
            InitializeComponent();
            
            this.user = user;
            try
            {
                User.load_User(ref user);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logs logs = new Logs();
                logs.writeException(ex);
            }
            

            this.welcomeLbl.Text += " " + user.name_;
        }
        #endregion


        private void Chatting_Load(object sender, EventArgs e)
        {
            try
            {
                
                cancellationTokenSource = new CancellationTokenSource();
                updateContent = new Task(() => displayMessageProcess(cancellationTokenSource.Token), cancellationTokenSource.Token, TaskCreationOptions.LongRunning);
                updateContent.Start();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logs logs = new Logs();
                logs.writeException(ex);
                this.Close();
            }
        }


        void displayMessageProcess(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    if (Listener.messages.TryTake(out string temp))
                        content.BeginInvoke((Action)(() => updateMessageBox(temp)));
                }
            }
            catch(Exception ex)
            {
                Logs logs = new Logs();
                logs.writeException(ex);
            }
        }
        
        void updateMessageBox(string newMessage)
        {
            content.Text += newMessage + "\n";
        }

        protected override void OnClosed(EventArgs e)
        {
            try
            {
                cancellationTokenSource.Cancel();
                updateContent.Wait();
                updateContent.Dispose();
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