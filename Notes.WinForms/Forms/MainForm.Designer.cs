namespace Notes.WinForms.Forms
{
    partial class MainForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.listSharedNotes = new System.Windows.Forms.ListBox();
            this.btnCreateNote = new System.Windows.Forms.Button();
            this.btnShareNote = new System.Windows.Forms.Button();
            this.btnEditNote = new System.Windows.Forms.Button();
            this.btnDeleteNote = new System.Windows.Forms.Button();
            this.btnUnshareNote = new System.Windows.Forms.Button();
            this.DGListNotes = new System.Windows.Forms.DataGridView();
            this.btnUpdateSharedNotes = new System.Windows.Forms.Button();
            this.btnFilter = new System.Windows.Forms.Button();
            this.btnClearFilter = new System.Windows.Forms.Button();
            this.btnOpenSharedNote = new System.Windows.Forms.Button();
            this.btnShowCategories = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnUpdateNotes = new System.Windows.Forms.Button();
            this.btnChangeUser = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DGListNotes)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(7, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Мои заметки";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(528, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Доступные мне";
            // 
            // listSharedNotes
            // 
            this.listSharedNotes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listSharedNotes.FormattingEnabled = true;
            this.listSharedNotes.Location = new System.Drawing.Point(532, 33);
            this.listSharedNotes.Name = "listSharedNotes";
            this.listSharedNotes.Size = new System.Drawing.Size(214, 238);
            this.listSharedNotes.TabIndex = 3;
            this.listSharedNotes.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listSharedNotes_DoubleClick);
            // 
            // btnCreateNote
            // 
            this.btnCreateNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCreateNote.Location = new System.Drawing.Point(11, 320);
            this.btnCreateNote.Name = "btnCreateNote";
            this.btnCreateNote.Size = new System.Drawing.Size(93, 23);
            this.btnCreateNote.TabIndex = 4;
            this.btnCreateNote.Text = "Новая заметка";
            this.btnCreateNote.UseVisualStyleBackColor = true;
            this.btnCreateNote.Click += new System.EventHandler(this.btnCreateNote_Click);
            // 
            // btnShareNote
            // 
            this.btnShareNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnShareNote.Location = new System.Drawing.Point(272, 320);
            this.btnShareNote.Name = "btnShareNote";
            this.btnShareNote.Size = new System.Drawing.Size(77, 23);
            this.btnShareNote.TabIndex = 5;
            this.btnShareNote.Text = "Поделиться";
            this.btnShareNote.UseVisualStyleBackColor = true;
            this.btnShareNote.Click += new System.EventHandler(this.btnShareNote_Click);
            // 
            // btnEditNote
            // 
            this.btnEditNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEditNote.Location = new System.Drawing.Point(110, 320);
            this.btnEditNote.Name = "btnEditNote";
            this.btnEditNote.Size = new System.Drawing.Size(75, 23);
            this.btnEditNote.TabIndex = 6;
            this.btnEditNote.Text = "Изменить";
            this.btnEditNote.UseVisualStyleBackColor = true;
            this.btnEditNote.Click += new System.EventHandler(this.btnEditNote_Click);
            // 
            // btnDeleteNote
            // 
            this.btnDeleteNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeleteNote.Location = new System.Drawing.Point(191, 320);
            this.btnDeleteNote.Name = "btnDeleteNote";
            this.btnDeleteNote.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteNote.TabIndex = 7;
            this.btnDeleteNote.Text = "Удалить";
            this.btnDeleteNote.UseVisualStyleBackColor = true;
            this.btnDeleteNote.Click += new System.EventHandler(this.btnDeleteNote_Click);
            // 
            // btnUnshareNote
            // 
            this.btnUnshareNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUnshareNote.Location = new System.Drawing.Point(355, 320);
            this.btnUnshareNote.Name = "btnUnshareNote";
            this.btnUnshareNote.Size = new System.Drawing.Size(75, 23);
            this.btnUnshareNote.TabIndex = 8;
            this.btnUnshareNote.Text = "Скрыть";
            this.btnUnshareNote.UseVisualStyleBackColor = true;
            this.btnUnshareNote.Click += new System.EventHandler(this.btnUnshareNote_Click);
            // 
            // DGListNotes
            // 
            this.DGListNotes.AllowUserToAddRows = false;
            this.DGListNotes.AllowUserToDeleteRows = false;
            this.DGListNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGListNotes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.DGListNotes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGListNotes.Location = new System.Drawing.Point(12, 34);
            this.DGListNotes.MultiSelect = false;
            this.DGListNotes.Name = "DGListNotes";
            this.DGListNotes.ReadOnly = true;
            this.DGListNotes.RowHeadersVisible = false;
            this.DGListNotes.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.DGListNotes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGListNotes.Size = new System.Drawing.Size(497, 273);
            this.DGListNotes.TabIndex = 9;
            this.DGListNotes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.listNotes_DoubleClick);
            this.DGListNotes.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.listNotes_DataBindingComplete);
            // 
            // btnUpdateSharedNotes
            // 
            this.btnUpdateSharedNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdateSharedNotes.Location = new System.Drawing.Point(613, 284);
            this.btnUpdateSharedNotes.Name = "btnUpdateSharedNotes";
            this.btnUpdateSharedNotes.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateSharedNotes.TabIndex = 10;
            this.btnUpdateSharedNotes.Text = "Обновить";
            this.btnUpdateSharedNotes.UseVisualStyleBackColor = true;
            this.btnUpdateSharedNotes.Click += new System.EventHandler(this.btnUpdateSharedNotes_Click);
            // 
            // btnFilter
            // 
            this.btnFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFilter.Location = new System.Drawing.Point(353, 7);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(75, 23);
            this.btnFilter.TabIndex = 11;
            this.btnFilter.Text = "Фильтр";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // btnClearFilter
            // 
            this.btnClearFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearFilter.Location = new System.Drawing.Point(434, 7);
            this.btnClearFilter.Name = "btnClearFilter";
            this.btnClearFilter.Size = new System.Drawing.Size(75, 23);
            this.btnClearFilter.TabIndex = 12;
            this.btnClearFilter.Text = "Сбросить";
            this.btnClearFilter.UseVisualStyleBackColor = true;
            this.btnClearFilter.Click += new System.EventHandler(this.btnClearFilter_Click);
            // 
            // btnOpenSharedNote
            // 
            this.btnOpenSharedNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenSharedNote.Location = new System.Drawing.Point(532, 284);
            this.btnOpenSharedNote.Name = "btnOpenSharedNote";
            this.btnOpenSharedNote.Size = new System.Drawing.Size(75, 23);
            this.btnOpenSharedNote.TabIndex = 13;
            this.btnOpenSharedNote.Text = "Открыть";
            this.btnOpenSharedNote.UseVisualStyleBackColor = true;
            this.btnOpenSharedNote.Click += new System.EventHandler(this.btnOpenSharedNote_Click);
            // 
            // btnShowCategories
            // 
            this.btnShowCategories.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowCategories.Location = new System.Drawing.Point(272, 7);
            this.btnShowCategories.Name = "btnShowCategories";
            this.btnShowCategories.Size = new System.Drawing.Size(75, 23);
            this.btnShowCategories.TabIndex = 14;
            this.btnShowCategories.Text = "Категории";
            this.btnShowCategories.UseVisualStyleBackColor = true;
            this.btnShowCategories.Click += new System.EventHandler(this.btnShowCategories_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(671, 320);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 15;
            this.btnExit.Text = "Выход";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnUpdateNotes
            // 
            this.btnUpdateNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUpdateNotes.Location = new System.Drawing.Point(436, 320);
            this.btnUpdateNotes.Name = "btnUpdateNotes";
            this.btnUpdateNotes.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateNotes.TabIndex = 16;
            this.btnUpdateNotes.Text = "Обновить";
            this.btnUpdateNotes.UseVisualStyleBackColor = true;
            this.btnUpdateNotes.Click += new System.EventHandler(this.btnUpdateNotes_Click);
            // 
            // btnChangeUser
            // 
            this.btnChangeUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChangeUser.Location = new System.Drawing.Point(532, 320);
            this.btnChangeUser.Name = "btnChangeUser";
            this.btnChangeUser.Size = new System.Drawing.Size(133, 23);
            this.btnChangeUser.TabIndex = 17;
            this.btnChangeUser.Text = "Сменить пользователя";
            this.btnChangeUser.UseVisualStyleBackColor = true;
            this.btnChangeUser.Click += new System.EventHandler(this.btnChangeUser_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 355);
            this.Controls.Add(this.btnChangeUser);
            this.Controls.Add(this.btnUpdateNotes);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnShowCategories);
            this.Controls.Add(this.btnOpenSharedNote);
            this.Controls.Add(this.btnClearFilter);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.btnUpdateSharedNotes);
            this.Controls.Add(this.DGListNotes);
            this.Controls.Add(this.btnUnshareNote);
            this.Controls.Add(this.btnDeleteNote);
            this.Controls.Add(this.btnEditNote);
            this.Controls.Add(this.btnShareNote);
            this.Controls.Add(this.btnCreateNote);
            this.Controls.Add(this.listSharedNotes);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "Заметки";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGListNotes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listSharedNotes;
        private System.Windows.Forms.Button btnCreateNote;
        private System.Windows.Forms.Button btnShareNote;
        private System.Windows.Forms.Button btnEditNote;
        private System.Windows.Forms.Button btnDeleteNote;
        private System.Windows.Forms.Button btnUnshareNote;
        private System.Windows.Forms.DataGridView DGListNotes;
        private System.Windows.Forms.Button btnUpdateSharedNotes;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Button btnClearFilter;
        private System.Windows.Forms.Button btnOpenSharedNote;
        private System.Windows.Forms.Button btnShowCategories;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnUpdateNotes;
        private System.Windows.Forms.Button btnChangeUser;
    }
}

