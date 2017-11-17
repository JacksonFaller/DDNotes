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
        public readonly List<Category> RemovedCategories = new List<Category>();
        public readonly List<Category> UpdatedCategories = new List<Category>();
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
            using (var form = new CreateOrEditCategoryForm())
            {
                if (form.ShowDialog() != DialogResult.OK) return;
                Categories.Add(_serviceClient.CreateCategory(form.CategoryName, _userId));
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
            var index = listCategories.SelectedIndex;
            _serviceClient.DeleteCategory(Categories[index].Id);
            RemovedCategories.Add(Categories[index]);
            UpdatedCategories.Remove(Categories[index]);
            Categories.RemoveAt(index);
        }

        private void btnChangeCategory_Click(object sender, EventArgs e)
        {
            var index = listCategories.SelectedIndex;
            using (var form = new CreateOrEditCategoryForm(Categories[index].Name))
            {
                form.Text = "Изменить категорию";
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Categories[index] = _serviceClient.UpdateCategory(Categories[index].Id, form.CategoryName);
                    UpdatedCategories.Add(Categories[index]);
                }
            }
        }
    }
}
