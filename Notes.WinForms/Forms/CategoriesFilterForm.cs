using Notes.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Notes.WinForms.Forms.BL;

namespace Notes.WinForms.Forms
{
    internal partial class CategoriesFilterForm : Form
    {
        public readonly CategoriesFilterFormBL FormBL;

        public CategoriesFilterForm(IEnumerable<Category> categories, IEnumerable<int> filteredCategoriesIndices, 
            ServiceClient client, int userId)
        {
            InitializeComponent();
            FormBL = new CategoriesFilterFormBL(client, userId, categories, filteredCategoriesIndices);
            listCategories.DataSource = FormBL.Categories;
            listCategories.DisplayMember = "Name";
        }

        private void btnClearSelection_Click(object sender, EventArgs e)
        {
            foreach (int index in listCategories.CheckedIndices)
            {
                listCategories.SetItemChecked(index, false);
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listCategories.Items.Count; i++)
            {
                listCategories.SetItemChecked(i, true);
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            FormBL.Filter(listCategories.CheckedIndices.Cast<int>());
        }

        private void btnUpdateCategories_Click(object sender, EventArgs e)
        {
            FormBL.UpdateCategories();
        }

        private void CategoriesFilterForm_Load(object sender, EventArgs e)
        {
            foreach (var index in FormBL.SelectedCategoriestIndices)
            {
                listCategories.SetItemChecked(index, true);
            }
        }
    }
}
