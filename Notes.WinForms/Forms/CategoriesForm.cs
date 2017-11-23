using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Notes.Model;
using Notes.WinForms.Forms.BL;

namespace Notes.WinForms.Forms
{
    internal partial class CategoriesForm : Form
    {
        public readonly CategoriesFormBL FormBL;

        public CategoriesForm(IEnumerable<Category> categories, int userId, ServiceClient client)
        {
            InitializeComponent();
            FormBL = new CategoriesFormBL(client, userId);
            FormBL.Categories.AddRange(categories);
            listCategories.DataSource = FormBL.Categories;
            listCategories.DisplayMember = "Name";
        }

        private void btnCreateCategory_Click(object sender, EventArgs e)
        {
            FormBL.CreateCategory();
        }

        private void btnUpdateCategories_Click(object sender, EventArgs e)
        {
            FormBL.UpdateCategories();
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            FormBL.DeleteCategory(listCategories.SelectedIndex);
        }

        private void btnUpdateCategory_Click(object sender, EventArgs e)
        {
            FormBL.UpdateCategory(listCategories.SelectedIndex);
        }
    }
}
