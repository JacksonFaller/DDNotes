namespace Notes.WinForms.Forms
{
    partial class StartForm
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
            this.btnCreateUser = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.userControl = new Notes.WinForms.UserControl();
            this.SuspendLayout();
            // 
            // btnCreateUser
            // 
            this.btnCreateUser.Location = new System.Drawing.Point(108, 87);
            this.btnCreateUser.Name = "btnCreateUser";
            this.btnCreateUser.Size = new System.Drawing.Size(134, 23);
            this.btnCreateUser.TabIndex = 2;
            this.btnCreateUser.Text = "Новый пользователь";
            this.btnCreateUser.UseVisualStyleBackColor = true;
            this.btnCreateUser.Click += new System.EventHandler(this.btnCreateUser_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(23, 87);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 1;
            this.btnLogin.Text = "Войти";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // userControl
            // 
            this.userControl.Location = new System.Drawing.Point(17, 12);
            this.userControl.Name = "userControl";
            this.userControl.Size = new System.Drawing.Size(229, 65);
            this.userControl.TabIndex = 0;
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 128);
            this.Controls.Add(this.userControl);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnCreateUser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "StartForm";
            this.Text = "StartForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StartForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCreateUser;
        private System.Windows.Forms.Button btnLogin;
        private UserControl userControl;
    }
}