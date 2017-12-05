namespace Notes.WinForms.Forms
{
    partial class CategoriesFilterForm
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
            this.btnFilter = new System.Windows.Forms.Button();
            this.btnClearSelection = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.listCategories = new System.Windows.Forms.CheckedListBox();
            this.btnUpdateCategories = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(8, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "Категории";
            // 
            // btnFilter
            // 
            this.btnFilter.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnFilter.Location = new System.Drawing.Point(12, 268);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(94, 23);
            this.btnFilter.TabIndex = 11;
            this.btnFilter.Text = "Отфильтровать";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // btnClearSelection
            // 
            this.btnClearSelection.Location = new System.Drawing.Point(127, 239);
            this.btnClearSelection.Name = "btnClearSelection";
            this.btnClearSelection.Size = new System.Drawing.Size(75, 23);
            this.btnClearSelection.TabIndex = 11;
            this.btnClearSelection.Text = "Снять все";
            this.btnClearSelection.UseVisualStyleBackColor = true;
            this.btnClearSelection.Click += new System.EventHandler(this.btnClearSelection_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(127, 268);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(12, 239);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(94, 23);
            this.btnSelectAll.TabIndex = 13;
            this.btnSelectAll.Text = "Выделить все";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // listCategories
            // 
            this.listCategories.CheckOnClick = true;
            this.listCategories.FormattingEnabled = true;
            this.listCategories.Location = new System.Drawing.Point(12, 41);
            this.listCategories.Name = "listCategories";
            this.listCategories.Size = new System.Drawing.Size(190, 184);
            this.listCategories.TabIndex = 14;
            // 
            // btnUpdateCategories
            // 
            this.btnUpdateCategories.Location = new System.Drawing.Point(127, 12);
            this.btnUpdateCategories.Name = "btnUpdateCategories";
            this.btnUpdateCategories.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateCategories.TabIndex = 15;
            this.btnUpdateCategories.Text = "Обновить";
            this.btnUpdateCategories.UseVisualStyleBackColor = true;
            this.btnUpdateCategories.Click += new System.EventHandler(this.btnUpdateCategories_Click);
            // 
            // CategoriesFilterForm
            // 
            this.AcceptButton = this.btnFilter;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(214, 302);
            this.Controls.Add(this.btnUpdateCategories);
            this.Controls.Add(this.listCategories);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnClearSelection);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "CategoriesFilterForm";
            this.Text = "Фильтр";
            this.Load += new System.EventHandler(this.CategoriesFilterForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Button btnClearSelection;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.CheckedListBox listCategories;
        private System.Windows.Forms.Button btnUpdateCategories;
    }
}