using Notes.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Notes.WinForms.Forms
{
    internal partial class CategoriesFilterForm : Form
    {
        public readonly BindingList<Category> Categories = new BindingList<Category>();
        public List<int> SelectedCategoriestIndices = new List<int>();
        private readonly ServiceClient _serviceClient;
        private readonly int _userId;

        public CategoriesFilterForm(IEnumerable<Category> categories, 
            IEnumerable<int> filteredCategoriesIndices, ServiceClient client, int userId)
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

            foreach (var index in filteredCategoriesIndices)
            {
                SelectedCategoriestIndices.Add(index);
            }
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
            SelectedCategoriestIndices.Clear();

            foreach (int index in listCategories.CheckedIndices)
            {
                SelectedCategoriestIndices.Add(index);
            }
        }

        private void btnUpdateCategories_Click(object sender, EventArgs e)
        {
            foreach (var category in _serviceClient.GetUserCategories(_userId))
            {
                Categories.Add(category);
            }
        }

        private void CategoriesFilterForm_Load(object sender, EventArgs e)
        {
            foreach (var index in SelectedCategoriestIndices)
            {
                listCategories.SetItemChecked(index, true);
            }
        }
    }
}
