using System;

namespace ChitChat
{
    partial class Invite
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.addBtn = new System.Windows.Forms.Button();
            this.usrLbl = new System.Windows.Forms.Label();
            this.usrname = new System.Windows.Forms.TextBox();
            this.toBeInvited = new System.Windows.Forms.ListBox();
            this.sendInvi = new System.Windows.Forms.Button();
            this.rmBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // addBtn
            // 
            this.addBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.addBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.addBtn.BackColor = System.Drawing.Color.Orange;
            this.addBtn.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addBtn.Location = new System.Drawing.Point(288, 25);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(97, 34);
            this.addBtn.TabIndex = 10;
            this.addBtn.Text = "Add";
            this.addBtn.UseVisualStyleBackColor = false;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // usrLbl
            // 
            this.usrLbl.BackColor = System.Drawing.Color.AliceBlue;
            this.usrLbl.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usrLbl.Location = new System.Drawing.Point(12, 31);
            this.usrLbl.Name = "usrLbl";
            this.usrLbl.Size = new System.Drawing.Size(100, 23);
            this.usrLbl.TabIndex = 11;
            this.usrLbl.Text = "Username:";
            this.usrLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // usrname
            // 
            this.usrname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.usrname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.usrname.Location = new System.Drawing.Point(118, 35);
            this.usrname.Name = "usrname";
            this.usrname.Size = new System.Drawing.Size(164, 20);
            this.usrname.TabIndex = 9;
            // 
            // toBeInvited
            // 
            this.toBeInvited.FormattingEnabled = true;
            this.toBeInvited.Location = new System.Drawing.Point(16, 83);
            this.toBeInvited.Name = "toBeInvited";
            this.toBeInvited.Size = new System.Drawing.Size(222, 147);
            this.toBeInvited.TabIndex = 12;
            // 
            // sendInvi
            // 
            this.sendInvi.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.sendInvi.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.sendInvi.BackColor = System.Drawing.Color.Orange;
            this.sendInvi.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sendInvi.Location = new System.Drawing.Point(269, 196);
            this.sendInvi.Name = "sendInvi";
            this.sendInvi.Size = new System.Drawing.Size(147, 34);
            this.sendInvi.TabIndex = 13;
            this.sendInvi.Text = "Send Invitation";
            this.sendInvi.UseVisualStyleBackColor = false;
            this.sendInvi.Click += new System.EventHandler(this.sendInvi_Click);
            // 
            // rmBtn
            // 
            this.rmBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rmBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.rmBtn.BackColor = System.Drawing.Color.Orange;
            this.rmBtn.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rmBtn.Location = new System.Drawing.Point(288, 83);
            this.rmBtn.Name = "rmBtn";
            this.rmBtn.Size = new System.Drawing.Size(97, 34);
            this.rmBtn.TabIndex = 14;
            this.rmBtn.Text = "Remove";
            this.rmBtn.UseVisualStyleBackColor = false;
            this.rmBtn.Click += new System.EventHandler(this.rmBtn_Click);
            // 
            // Invite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 242);
            this.Controls.Add(this.rmBtn);
            this.Controls.Add(this.sendInvi);
            this.Controls.Add(this.toBeInvited);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.usrLbl);
            this.Controls.Add(this.usrname);
            this.Name = "Invite";
            this.Text = "Invite";
            this.Load += new System.EventHandler(this.Invite_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.Label usrLbl;
        private System.Windows.Forms.TextBox usrname;
        private System.Windows.Forms.ListBox toBeInvited;
        private System.Windows.Forms.Button sendInvi;
        private System.Windows.Forms.Button rmBtn;
    }
}