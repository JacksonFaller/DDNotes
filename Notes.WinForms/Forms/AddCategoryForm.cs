using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Notes.Model;

namespace Notes.WinForms.Forms
{
    internal partial class AddCategoryForm : Form
    {
        private readonly ServiceClient _serviceClient;
        private readonly int _userId;
        public AddCategoryForm(ServiceClient client, int userId)
        {
            InitializeComponent();
            _serviceClient = client;
            _userId = userId;
        }
        public BindingList<Category> Categories = new BindingList<Category>();

        public List<int> SelectedCategoriesIndices = new List<int>();
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

        private void AddCategoryForm_Load(object sender, EventArgs e)
        {
            listCategories.DataSource = Categories; 
            listCategories.DisplayMember = "Name";

            foreach (var category in _serviceClient.GetUserCategories(_userId))
            {
                Categories.Add(category);
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
    }
}
