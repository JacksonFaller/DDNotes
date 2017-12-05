namespace Notes.WinForms.Forms
{
    partial class CreateOrEditNoteForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtNoteTitle = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNoteText = new System.Windows.Forms.RichTextBox();
            this.btnSaveNote = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.listNoteCategories = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAddCategory = new System.Windows.Forms.Button();
            this.btnDeleteCategory = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Заголовок";
            // 
            // txtNoteTitle
            // 
            this.txtNoteTitle.Location = new System.Drawing.Point(119, 12);
            this.txtNoteTitle.Name = "txtNoteTitle";
            this.txtNoteTitle.Size = new System.Drawing.Size(325, 20);
            this.txtNoteTitle.TabIndex = 2;
            this.txtNoteTitle.TabStop = false;
            this.txtNoteTitle.TextChanged += new System.EventHandler(this.txtNoteTitle_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Текст";
            // 
            // txtNoteText
            // 
            this.txtNoteText.AcceptsTab = true;
            this.txtNoteText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNoteText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoteText.Location = new System.Drawing.Point(16, 61);
            this.txtNoteText.Name = "txtNoteText";
            this.txtNoteText.Size = new System.Drawing.Size(431, 246);
            this.txtNoteText.TabIndex = 4;
            this.txtNoteText.Text = "";
            this.txtNoteText.TextChanged += new System.EventHandler(this.txtNoteText_TextChanged);
            // 
            // btnSaveNote
            // 
            this.btnSaveNote.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSaveNote.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSaveNote.Location = new System.Drawing.Point(213, 322);
            this.btnSaveNote.Name = "btnSaveNote";
            this.btnSaveNote.Size = new System.Drawing.Size(75, 23);
            this.btnSaveNote.TabIndex = 5;
            this.btnSaveNote.Text = "Сохранить";
            this.btnSaveNote.UseVisualStyleBackColor = true;
            this.btnSaveNote.Click += new System.EventHandler(this.btnSaveNote_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(335, 322);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // listNoteCategories
            // 
            this.listNoteCategories.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listNoteCategories.FormattingEnabled = true;
            this.listNoteCategories.Location = new System.Drawing.Point(456, 62);
            this.listNoteCategories.Name = "listNoteCategories";
            this.listNoteCategories.Size = new System.Drawing.Size(158, 212);
            this.listNoteCategories.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(453, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Категории";
            // 
            // btnAddCategory
            // 
            this.btnAddCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddCategory.Location = new System.Drawing.Point(456, 287);
            this.btnAddCategory.Name = "btnAddCategory";
            this.btnAddCategory.Size = new System.Drawing.Size(75, 23);
            this.btnAddCategory.TabIndex = 9;
            this.btnAddCategory.Text = "Добавить";
            this.btnAddCategory.UseVisualStyleBackColor = true;
            this.btnAddCategory.Click += new System.EventHandler(this.btnAddCategory_Click);
            // 
            // btnDeleteCategory
            // 
            this.btnDeleteCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteCategory.Location = new System.Drawing.Point(539, 287);
            this.btnDeleteCategory.Name = "btnDeleteCategory";
            this.btnDeleteCategory.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteCategory.TabIndex = 10;
            this.btnDeleteCategory.Text = "Удалить";
            this.btnDeleteCategory.UseVisualStyleBackColor = true;
            this.btnDeleteCategory.Click += new System.EventHandler(this.btnDeleteCategory_Click);
            // 
            // CreateOrEditNoteForm
            // 
            this.AcceptButton = this.btnSaveNote;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(627, 357);
            this.Controls.Add(this.btnDeleteCategory);
            this.Controls.Add(this.btnAddCategory);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listNoteCategories);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSaveNote);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNoteTitle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNoteText);
            this.Name = "CreateOrEditNoteForm";
            this.Text = "Создать заметку";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNoteTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox txtNoteText;
        private System.Windows.Forms.Button btnSaveNote;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ListBox listNoteCategories;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAddCategory;
        private System.Windows.Forms.Button btnDeleteCategory;
    }
}