using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Notes.Model;

namespace Notes.WinForms.Forms
{
    internal partial class AddCategoryForm : Form
    {
        private readonly ServiceClient _serviceClient;
        private readonly int _userId;

        public BindingList<Category> Categories = new BindingList<Category>();
        public List<int> SelectedCategoriesIndices = new List<int>();

        public AddCategoryForm(IEnumerable<Category> categories,  ServiceClient client, int userId)
        {
            InitializeComponent();
            listCategories.DataSource = Categories;
            listCategories.DisplayMember = "Name";
            _serviceClient = client;
            _userId = userId;

            foreach (var category in categories)
            {
                Categories.Add(category);
            }
        }
        
        private void btnCreateCategory_Click(object sender, EventArgs e)
        {
            using (var form = new CreateCategoryForm())
            {
                if (form.ShowDialog() != DialogResult.OK) return;

                SelectedCategoriesIndices.Clear();
                foreach (int index in listCategories.CheckedIndices)
                {
                    SelectedCategoriesIndices.Add(index);
                }
                SelectedCategoriesIndices.Add(Categories.Count);
                Categories.Add(_serviceClient.CreateCategory(form.CategoryName, _userId));

                foreach (var index in SelectedCategoriesIndices)
                {
                    listCategories.SetItemChecked(index, true);
                }
            }
        }

        private void btnAddCategories_Click(object sender, EventArgs e)
        {
            SelectedCategoriesIndices.Clear();
            foreach (int index in listCategories.CheckedIndices)
            {
                SelectedCategoriesIndices.Add(index);
            }
        }

        private void btnUpdateCategories_Click(object sender, EventArgs e)
        {
            Categories.Clear();
            foreach (var category in _serviceClient.GetUserCategories(_userId))
            {
                Categories.Add(category);
            }
        }
    }
}
