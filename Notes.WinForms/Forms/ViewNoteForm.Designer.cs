namespace Notes.WinForms.Forms
{
    partial class ViewNoteForm
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
            this.label3 = new System.Windows.Forms.Label();
            this.listNoteCategories = new System.Windows.Forms.ListBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNoteTitle = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNoteText = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(440, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 20);
            this.label3.TabIndex = 18;
            this.label3.Text = "Категории";
            // 
            // listNoteCategories
            // 
            this.listNoteCategories.Enabled = false;
            this.listNoteCategories.FormattingEnabled = true;
            this.listNoteCategories.Location = new System.Drawing.Point(445, 67);
            this.listNoteCategories.Name = "listNoteCategories";
            this.listNoteCategories.Size = new System.Drawing.Size(160, 225);
            this.listNoteCategories.TabIndex = 17;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.Location = new System.Drawing.Point(252, 306);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "Закрыть";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(2, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "Заголовок";
            // 
            // txtNoteTitle
            // 
            this.txtNoteTitle.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtNoteTitle.HideSelection = false;
            this.txtNoteTitle.Location = new System.Drawing.Point(113, 13);
            this.txtNoteTitle.Name = "txtNoteTitle";
            this.txtNoteTitle.ReadOnly = true;
            this.txtNoteTitle.Size = new System.Drawing.Size(325, 20);
            this.txtNoteTitle.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(2, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 20);
            this.label2.TabIndex = 13;
            this.label2.Text = "Текст";
            // 
            // txtNoteText
            // 
            this.txtNoteText.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtNoteText.Location = new System.Drawing.Point(6, 67);
            this.txtNoteText.Name = "txtNoteText";
            this.txtNoteText.ReadOnly = true;
            this.txtNoteText.Size = new System.Drawing.Size(432, 225);
            this.txtNoteText.TabIndex = 14;
            this.txtNoteText.Text = "";
            // 
            // ViewNoteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 341);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listNoteCategories);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNoteTitle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNoteText);
            this.Name = "ViewNoteForm";
            this.Text = "Просмотр заметки";
            this.Load += new System.EventHandler(this.ViewNoteForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listNoteCategories;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNoteTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox txtNoteText;
    }
}