namespace ChitChat
{
    partial class GroupChat
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
            this.components = new System.ComponentModel.Container();
            this.content = new System.Windows.Forms.RichTextBox();
            this.sendBtn = new System.Windows.Forms.Button();
            this.welcomeLbl = new System.Windows.Forms.Label();
            this.send = new System.Windows.Forms.RichTextBox();
            this.members = new System.Windows.Forms.ListBox();
            this.inviteBtn = new System.Windows.Forms.Button();
            this.displayMessages = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // content
            // 
            this.content.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.content.BackColor = System.Drawing.SystemColors.Window;
            this.content.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.content.Location = new System.Drawing.Point(13, 60);
            this.content.MaxLength = 0;
            this.content.Name = "content";
            this.content.ReadOnly = true;
            this.content.Size = new System.Drawing.Size(295, 236);
            this.content.TabIndex = 12;
            this.content.Text = "";
            // 
            // sendBtn
            // 
            this.sendBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sendBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.sendBtn.BackColor = System.Drawing.Color.Orange;
            this.sendBtn.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sendBtn.Location = new System.Drawing.Point(211, 400);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(97, 38);
            this.sendBtn.TabIndex = 11;
            this.sendBtn.Text = "Send";
            this.sendBtn.UseVisualStyleBackColor = false;
            this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
            // 
            // welcomeLbl
            // 
            this.welcomeLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.welcomeLbl.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.welcomeLbl.Location = new System.Drawing.Point(10, 11);
            this.welcomeLbl.Name = "welcomeLbl";
            this.welcomeLbl.Size = new System.Drawing.Size(486, 46);
            this.welcomeLbl.TabIndex = 10;
            this.welcomeLbl.Text = "Chatting with ";
            this.welcomeLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // send
            // 
            this.send.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.send.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.send.Location = new System.Drawing.Point(13, 312);
            this.send.Name = "send";
            this.send.Size = new System.Drawing.Size(295, 80);
            this.send.TabIndex = 9;
            this.send.Text = "";
            // 
            // members
            // 
            this.members.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.members.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.members.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.members.FormattingEnabled = true;
            this.members.ItemHeight = 18;
            this.members.Location = new System.Drawing.Point(314, 60);
            this.members.Name = "members";
            this.members.Size = new System.Drawing.Size(182, 236);
            this.members.TabIndex = 13;
            // 
            // inviteBtn
            // 
            this.inviteBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inviteBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.inviteBtn.BackColor = System.Drawing.Color.Orange;
            this.inviteBtn.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inviteBtn.Location = new System.Drawing.Point(399, 400);
            this.inviteBtn.Name = "inviteBtn";
            this.inviteBtn.Size = new System.Drawing.Size(97, 38);
            this.inviteBtn.TabIndex = 14;
            this.inviteBtn.Text = "Invite";
            this.inviteBtn.UseVisualStyleBackColor = false;
            // 
            // GroupChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 450);
            this.Controls.Add(this.inviteBtn);
            this.Controls.Add(this.members);
            this.Controls.Add(this.content);
            this.Controls.Add(this.sendBtn);
            this.Controls.Add(this.welcomeLbl);
            this.Controls.Add(this.send);
            this.Name = "GroupChat";
            this.Text = "GroupChat";
            this.ResumeLayout(false);
            this.Load += GroupChat_Load;

        }

        #endregion

        private System.Windows.Forms.RichTextBox content;
        private System.Windows.Forms.Button sendBtn;
        private System.Windows.Forms.Label welcomeLbl;
        private System.Windows.Forms.RichTextBox send;
        private System.Windows.Forms.ListBox members;
        private System.Windows.Forms.Button inviteBtn;
        private System.Windows.Forms.Timer displayMessages;
    }
}