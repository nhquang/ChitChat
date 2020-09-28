namespace ChitChat
{
    partial class SelectFriendToChat
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
            this.welcomeLbl = new System.Windows.Forms.Label();
            this.contacts = new System.Windows.Forms.ListBox();
            this.chatBtn = new System.Windows.Forms.Button();
            this.notification = new System.Windows.Forms.Label();
            this.addCtBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // welcomeLbl
            // 
            this.welcomeLbl.Font = new System.Drawing.Font("Verdana", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.welcomeLbl.Location = new System.Drawing.Point(12, 9);
            this.welcomeLbl.Name = "welcomeLbl";
            this.welcomeLbl.Size = new System.Drawing.Size(222, 46);
            this.welcomeLbl.TabIndex = 3;
            this.welcomeLbl.Text = "Hi ";
            this.welcomeLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // contacts
            // 
            this.contacts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.contacts.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contacts.FormattingEnabled = true;
            this.contacts.ItemHeight = 20;
            this.contacts.Location = new System.Drawing.Point(12, 93);
            this.contacts.Name = "contacts";
            this.contacts.Size = new System.Drawing.Size(304, 182);
            this.contacts.TabIndex = 4;
            // 
            // chatBtn
            // 
            this.chatBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chatBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.chatBtn.BackColor = System.Drawing.Color.Orange;
            this.chatBtn.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chatBtn.Location = new System.Drawing.Point(219, 292);
            this.chatBtn.Name = "chatBtn";
            this.chatBtn.Size = new System.Drawing.Size(97, 43);
            this.chatBtn.TabIndex = 8;
            this.chatBtn.Text = "Chat";
            this.chatBtn.UseVisualStyleBackColor = false;
            // 
            // notification
            // 
            this.notification.BackColor = System.Drawing.Color.White;
            this.notification.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.notification.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.notification.Location = new System.Drawing.Point(357, 93);
            this.notification.Name = "notification";
            this.notification.Size = new System.Drawing.Size(189, 182);
            this.notification.TabIndex = 9;
            this.notification.Text = "Notification:";
            // 
            // addCtBtn
            // 
            this.addCtBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.addCtBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.addCtBtn.BackColor = System.Drawing.Color.Orange;
            this.addCtBtn.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addCtBtn.Location = new System.Drawing.Point(12, 292);
            this.addCtBtn.Name = "addCtBtn";
            this.addCtBtn.Size = new System.Drawing.Size(126, 43);
            this.addCtBtn.TabIndex = 10;
            this.addCtBtn.Text = "Add Contact";
            this.addCtBtn.UseVisualStyleBackColor = false;
            this.addCtBtn.Click += new System.EventHandler(this.addCtBtn_Click);
            // 
            // SelectFriendToChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(558, 347);
            this.Controls.Add(this.addCtBtn);
            this.Controls.Add(this.notification);
            this.Controls.Add(this.chatBtn);
            this.Controls.Add(this.contacts);
            this.Controls.Add(this.welcomeLbl);
            this.Name = "SelectFriendToChat";
            this.Text = "SelectFriendToChat";
            this.Load += new System.EventHandler(this.SelectFriendToChat_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label welcomeLbl;
        private System.Windows.Forms.ListBox contacts;
        private System.Windows.Forms.Button chatBtn;
        private System.Windows.Forms.Label notification;
        private System.Windows.Forms.Button addCtBtn;
    }
}