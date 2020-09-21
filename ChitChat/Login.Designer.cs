namespace ChitChat
{
    partial class Login
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
            this.loginLbl = new System.Windows.Forms.Label();
            this.usrLbl = new System.Windows.Forms.Label();
            this.pwdLbl = new System.Windows.Forms.Label();
            this.usr = new System.Windows.Forms.TextBox();
            this.pwd = new System.Windows.Forms.TextBox();
            this.signInBtn = new System.Windows.Forms.Button();
            this.regBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // loginLbl
            // 
            this.loginLbl.BackColor = System.Drawing.Color.Orange;
            this.loginLbl.Dock = System.Windows.Forms.DockStyle.Left;
            this.loginLbl.Font = new System.Drawing.Font("Unispace", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginLbl.Location = new System.Drawing.Point(0, 0);
            this.loginLbl.Name = "loginLbl";
            this.loginLbl.Size = new System.Drawing.Size(166, 411);
            this.loginLbl.TabIndex = 0;
            this.loginLbl.Text = "Log-In";
            this.loginLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // usrLbl
            // 
            this.usrLbl.BackColor = System.Drawing.Color.AliceBlue;
            this.usrLbl.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usrLbl.Location = new System.Drawing.Point(188, 93);
            this.usrLbl.Name = "usrLbl";
            this.usrLbl.Size = new System.Drawing.Size(100, 23);
            this.usrLbl.TabIndex = 1;
            this.usrLbl.Text = "Username:";
            this.usrLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pwdLbl
            // 
            this.pwdLbl.BackColor = System.Drawing.Color.AliceBlue;
            this.pwdLbl.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwdLbl.Location = new System.Drawing.Point(188, 177);
            this.pwdLbl.Name = "pwdLbl";
            this.pwdLbl.Size = new System.Drawing.Size(100, 23);
            this.pwdLbl.TabIndex = 2;
            this.pwdLbl.Text = "Password:";
            this.pwdLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // usr
            // 
            this.usr.Location = new System.Drawing.Point(323, 93);
            this.usr.Name = "usr";
            this.usr.Size = new System.Drawing.Size(228, 20);
            this.usr.TabIndex = 3;
            // 
            // pwd
            // 
            this.pwd.Location = new System.Drawing.Point(323, 177);
            this.pwd.Name = "pwd";
            this.pwd.PasswordChar = '*';
            this.pwd.Size = new System.Drawing.Size(228, 20);
            this.pwd.TabIndex = 4;
            // 
            // signInBtn
            // 
            this.signInBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.signInBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.signInBtn.BackColor = System.Drawing.Color.Orange;
            this.signInBtn.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.signInBtn.Location = new System.Drawing.Point(192, 284);
            this.signInBtn.Name = "signInBtn";
            this.signInBtn.Size = new System.Drawing.Size(155, 38);
            this.signInBtn.TabIndex = 6;
            this.signInBtn.Text = "Sign in";
            this.signInBtn.UseVisualStyleBackColor = false;
            this.signInBtn.Click += new System.EventHandler(this.signInBtn_Click);
            // 
            // regBtn
            // 
            this.regBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.regBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.regBtn.BackColor = System.Drawing.Color.Orange;
            this.regBtn.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.regBtn.Location = new System.Drawing.Point(396, 284);
            this.regBtn.Name = "regBtn";
            this.regBtn.Size = new System.Drawing.Size(155, 38);
            this.regBtn.TabIndex = 7;
            this.regBtn.Text = "Register";
            this.regBtn.UseVisualStyleBackColor = false;
            this.regBtn.Click += new System.EventHandler(this.regBtn_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(634, 411);
            this.Controls.Add(this.regBtn);
            this.Controls.Add(this.signInBtn);
            this.Controls.Add(this.pwd);
            this.Controls.Add(this.usr);
            this.Controls.Add(this.pwdLbl);
            this.Controls.Add(this.usrLbl);
            this.Controls.Add(this.loginLbl);
            this.Name = "Login";
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label loginLbl;
        private System.Windows.Forms.Label usrLbl;
        private System.Windows.Forms.Label pwdLbl;
        private System.Windows.Forms.TextBox usr;
        private System.Windows.Forms.TextBox pwd;
        private System.Windows.Forms.Button signInBtn;
        private System.Windows.Forms.Button regBtn;
    }
}