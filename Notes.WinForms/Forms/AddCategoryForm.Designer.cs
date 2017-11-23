namespace Notes.WinForms.Forms
{
    partial class AddCategoryForm
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
            this.listCategories = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddCategories = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnCreateCategory = new System.Windows.Forms.Button();
            this.btnUpdateCategories = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listCategories
            // 
            this.listCategories.CheckOnClick = true;
            this.listCategories.FormattingEnabled = true;
            this.listCategories.Location = new System.Drawing.Point(7, 25);
            this.listCategories.Name = "listCategories";
            this.listCategories.Size = new System.Drawing.Size(203, 154);
            this.listCategories.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(4, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Мои категории";
            // 
            // btnAddCategories
            // 
            this.btnAddCategories.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAddCategories.Location = new System.Drawing.Point(7, 223);
            this.btnAddCategories.Name = "btnAddCategories";
            this.btnAddCategories.Size = new System.Drawing.Size(75, 23);
            this.btnAddCategories.TabIndex = 2;
            this.btnAddCategories.Text = "Добавить";
            this.btnAddCategories.UseVisualStyleBackColor = true;
            this.btnAddCategories.Click += new System.EventHandler(this.btnAddCategories_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(135, 223);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnCreateCategory
            // 
            this.btnCreateCategory.Location = new System.Drawing.Point(7, 185);
            this.btnCreateCategory.Name = "btnCreateCategory";
            this.btnCreateCategory.Size = new System.Drawing.Size(105, 23);
            this.btnCreateCategory.TabIndex = 4;
            this.btnCreateCategory.Text = "Новая категория";
            this.btnCreateCategory.UseVisualStyleBackColor = true;
            this.btnCreateCategory.Click += new System.EventHandler(this.btnCreateCategory_Click);
            // 
            // btnUpdateCategories
            // 
            this.btnUpdateCategories.Location = new System.Drawing.Point(135, 185);
            this.btnUpdateCategories.Name = "btnUpdateCategories";
            this.btnUpdateCategories.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateCategories.TabIndex = 5;
            this.btnUpdateCategories.Text = "Обновить";
            this.btnUpdateCategories.UseVisualStyleBackColor = true;
            this.btnUpdateCategories.Click += new System.EventHandler(this.btnUpdateCategories_Click);
            // 
            // AddCategoryForm
            // 
            this.AcceptButton = this.btnAddCategories;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(218, 258);
            this.Controls.Add(this.btnUpdateCategories);
            this.Controls.Add(this.btnCreateCategory);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAddCategories);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listCategories);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "AddCategoryForm";
            this.Text = "Добавить категории";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox listCategories;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddCategories;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnCreateCategory;
        private System.Windows.Forms.Button btnUpdateCategories;
    }
}