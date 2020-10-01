namespace ChitChat
{
    partial class Chatting
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
            this.send = new System.Windows.Forms.RichTextBox();
            this.welcomeLbl = new System.Windows.Forms.Label();
            this.sendBtn = new System.Windows.Forms.Button();
            this.timer_ = new System.Windows.Forms.Timer(this.components);
            this.content = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // send
            // 
            this.send.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.send.Location = new System.Drawing.Point(12, 310);
            this.send.Name = "send";
            this.send.Size = new System.Drawing.Size(482, 80);
            this.send.TabIndex = 1;
            this.send.Text = "";
            // 
            // welcomeLbl
            // 
            this.welcomeLbl.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.welcomeLbl.Location = new System.Drawing.Point(9, 9);
            this.welcomeLbl.Name = "welcomeLbl";
            this.welcomeLbl.Size = new System.Drawing.Size(486, 46);
            this.welcomeLbl.TabIndex = 2;
            this.welcomeLbl.Text = "Chatting with ";
            this.welcomeLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // sendBtn
            // 
            this.sendBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.sendBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.sendBtn.BackColor = System.Drawing.Color.Orange;
            this.sendBtn.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sendBtn.Location = new System.Drawing.Point(398, 400);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(97, 38);
            this.sendBtn.TabIndex = 7;
            this.sendBtn.Text = "Send";
            this.sendBtn.UseVisualStyleBackColor = false;
            this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
            // 
            // content
            // 
            this.content.BackColor = System.Drawing.SystemColors.Window;
            this.content.Location = new System.Drawing.Point(14, 79);
            this.content.Name = "content";
            this.content.Size = new System.Drawing.Size(480, 199);
            this.content.TabIndex = 8;
            // 
            // Chatting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(507, 450);
            this.Controls.Add(this.content);
            this.Controls.Add(this.sendBtn);
            this.Controls.Add(this.welcomeLbl);
            this.Controls.Add(this.send);
            this.Name = "Chatting";
            this.Text = "Chatting";
            this.Load += new System.EventHandler(this.Chatting_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RichTextBox send;
        private System.Windows.Forms.Label welcomeLbl;
        private System.Windows.Forms.Button sendBtn;
        private System.Windows.Forms.Timer timer_;
        private System.Windows.Forms.Label content;
    }
}