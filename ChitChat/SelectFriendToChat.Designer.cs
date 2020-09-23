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
            this.friends = new System.Windows.Forms.ListBox();
            this.chatBtn = new System.Windows.Forms.Button();
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
            // friends
            // 
            this.friends.FormattingEnabled = true;
            this.friends.Location = new System.Drawing.Point(12, 93);
            this.friends.Name = "friends";
            this.friends.Size = new System.Drawing.Size(304, 186);
            this.friends.TabIndex = 4;
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
            this.chatBtn.Text = "Send";
            this.chatBtn.UseVisualStyleBackColor = false;
            // 
            // SelectFriendToChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(558, 347);
            this.Controls.Add(this.chatBtn);
            this.Controls.Add(this.friends);
            this.Controls.Add(this.welcomeLbl);
            this.Name = "SelectFriendToChat";
            this.Text = "SelectFriendToChat";
            this.Load += new System.EventHandler(this.SelectFriendToChat_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label welcomeLbl;
        private System.Windows.Forms.ListBox friends;
        private System.Windows.Forms.Button chatBtn;
    }
}