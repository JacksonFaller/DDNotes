namespace Notes.WinForms.Forms
{
    partial class ShareNoteForm
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
            this.listUsers = new System.Windows.Forms.ListBox();
            this.btnUnshare = new System.Windows.Forms.Button();
            this.btnShare = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listUsers
            // 
            this.listUsers.FormattingEnabled = true;
            this.listUsers.Location = new System.Drawing.Point(12, 33);
            this.listUsers.Name = "listUsers";
            this.listUsers.Size = new System.Drawing.Size(181, 121);
            this.listUsers.TabIndex = 0;
            // 
            // btnUnshare
            // 
            this.btnUnshare.Location = new System.Drawing.Point(117, 204);
            this.btnUnshare.Name = "btnUnshare";
            this.btnUnshare.Size = new System.Drawing.Size(75, 23);
            this.btnUnshare.TabIndex = 1;
            this.btnUnshare.Text = "Скрыть";
            this.btnUnshare.UseVisualStyleBackColor = true;
            this.btnUnshare.Click += new System.EventHandler(this.btnUnshare_Click);
            // 
            // btnShare
            // 
            this.btnShare.Location = new System.Drawing.Point(13, 204);
            this.btnShare.Name = "btnShare";
            this.btnShare.Size = new System.Drawing.Size(79, 23);
            this.btnShare.TabIndex = 1;
            this.btnShare.Text = "Поделиться";
            this.btnShare.UseVisualStyleBackColor = true;
            this.btnShare.Click += new System.EventHandler(this.btnShare_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Список пользователей с доступом";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(13, 178);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(180, 20);
            this.txtUserName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 161);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Имя пользователя";
            // 
            // ShareNoteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(204, 239);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnShare);
            this.Controls.Add(this.btnUnshare);
            this.Controls.Add(this.listUsers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ShareNoteForm";
            this.Text = "Поделиться";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listUsers;
        private System.Windows.Forms.Button btnUnshare;
        private System.Windows.Forms.Button btnShare;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label2;
    }
}