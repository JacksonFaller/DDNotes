using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Notes.Model;
using Notes.WinForms.Forms.BL;

namespace Notes.WinForms.Forms
{
    internal partial class AddCategoryForm : Form
    {
        public BindingList<Category> Categories = new BindingList<Category>();
        public List<int> SelectedCategoriesIndices = new List<int>();
        private readonly AddCategoryFormBL _formBL;

        public AddCategoryForm(IEnumerable<Category> noteCategories,  ServiceClient client, User user)
        {
            InitializeComponent();
            listCategories.DataSource = Categories;
            listCategories.DisplayMember = "Name";
            var categories = noteCategories.ToList();
            _formBL = new AddCategoryFormBL(categories, client, user);
            Categories.AddRange(user.Categories.Except(categories, new MainFormBL.CategoriesComparer()));
        }
        
        private void btnCreateCategory_Click(object sender, EventArgs e)
        {
            using (var form = new CreateOrEditCategoryForm())
            {
                if (form.ShowDialog() != DialogResult.OK) return;
                _formBL.CreateCategory(SelectedCategoriesIndices, listCategories, Categories, form.CategoryName);
            }
        }

        private void btnAddCategories_Click(object sender, EventArgs e)
        {
            _formBL.AddCategories(SelectedCategoriesIndices, listCategories.CheckedIndices);
        }

        private void btnUpdateCategories_Click(object sender, EventArgs e)
        {
            _formBL.UpdateCategories(Categories);
        }
    }

}
