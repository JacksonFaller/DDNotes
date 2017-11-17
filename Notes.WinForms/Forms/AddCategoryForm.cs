using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Notes.Model;

namespace Notes.WinForms.Forms
{
    internal partial class AddCategoryForm : Form
    {
        private readonly ServiceClient _serviceClient;
        private readonly User _user;
        private readonly IEnumerable<Category> _noteCategories;

        public BindingList<Category> Categories = new BindingList<Category>();
        public List<int> SelectedCategoriesIndices = new List<int>();

        public AddCategoryForm(IEnumerable<Category> noteCategories,  ServiceClient client, User user)
        {
            InitializeComponent();
            listCategories.DataSource = Categories;
            listCategories.DisplayMember = "Name";
            _serviceClient = client;
            _user = user;
            _noteCategories = noteCategories;

            foreach (var category in user.Categories.Except(noteCategories, new MainForm.CategoriesComparer()).ToList())
            {
                Categories.Add(category);
            }
        }
        
        private void btnCreateCategory_Click(object sender, EventArgs e)
        {
            using (var form = new CreateOrEditCategoryForm())
            {
                if (form.ShowDialog() != DialogResult.OK) return;

                SelectedCategoriesIndices.Clear();
                foreach (int index in listCategories.CheckedIndices)
                {
                    SelectedCategoriesIndices.Add(index);
                }
                SelectedCategoriesIndices.Add(Categories.Count);
                Categories.Add(_serviceClient.CreateCategory(form.CategoryName, _user.Id));

                foreach (var index in SelectedCategoriesIndices)
                {
                    listCategories.SetItemChecked(index, true);
                }
                var categories = _user.Categories.ToList();
                categories.Add(Categories.Last());
               _user.Categories = categories;
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
            _user.Categories = _serviceClient.GetUserCategories(_user.Id);
            foreach (var category in _user.Categories.Except(_noteCategories, new MainForm.CategoriesComparer()))
            {
                Categories.Add(category); 
            }
        }
    }

}
