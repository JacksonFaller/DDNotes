using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Notes.Model;

namespace Notes.WinForms.Forms.BL
{
    class CategoriesFormBL
    {
        private readonly ServiceClient _serviceClient;
        private readonly int _userId;

        public readonly BindingList<Category> Categories = new BindingList<Category>();
        public readonly List<Category> RemovedCategories = new List<Category>();
        public readonly List<Category> UpdatedCategories = new List<Category>();

        public CategoriesFormBL(ServiceClient client, int userId)
        {
            _serviceClient = client;
            _userId = userId;
        }

        public void CreateCategory()
        {
            using (var form = new CreateOrEditCategoryForm())
            {
                if (form.ShowDialog() != DialogResult.OK) return;
                Categories.Add(_serviceClient.CreateCategory(form.CategoryName, _userId));
            }
        }

        public void UpdateCategories()
        {
            Categories.Clear();
            Categories.AddRange(_serviceClient.GetUserCategories(_userId));
        }

        public void DeleteCategory(int index)
        {
            _serviceClient.DeleteCategory(Categories[index].Id);
            RemovedCategories.Add(Categories[index]);
            UpdatedCategories.Remove(Categories[index]);
            Categories.RemoveAt(index);
        }

        public void UpdateCategory(int index)
        {
            using (var form = new CreateOrEditCategoryForm(Categories[index].Name))
            {
                form.Text = "Изменить категорию";
                if (form.ShowDialog() != DialogResult.OK) return;
                Categories[index] = _serviceClient.UpdateCategory(Categories[index].Id, form.CategoryName);
                UpdatedCategories.Add(Categories[index]);
            }
        }
    }
}
