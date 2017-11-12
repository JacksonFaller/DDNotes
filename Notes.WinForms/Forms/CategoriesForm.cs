using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Notes.Model;

namespace Notes.WinForms.Forms
{
    internal partial class CategoriesForm : Form
    {
        private readonly ServiceClient _serviceClient;
        private readonly int _userId;

        public readonly BindingList<Category> Categories = new BindingList<Category>();
        public readonly List<Category> RemovedCategoiesIds = new List<Category>();
        public bool IsCategoriesChanged;
        public CategoriesForm(IEnumerable<Category> categories, int userId, ServiceClient client)
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
                Categories.Add(_serviceClient.CreateCategory(form.CategoryName, _userId));
                IsCategoriesChanged = true;
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

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            _serviceClient.DeleteCategory(Categories[listCategories.SelectedIndex].Id);
            RemovedCategoiesIds.Add(Categories[listCategories.SelectedIndex]);
            Categories.RemoveAt(listCategories.SelectedIndex);
            IsCategoriesChanged = true;
        }
    }
}
