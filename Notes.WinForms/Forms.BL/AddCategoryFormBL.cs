using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Notes.Model;

namespace Notes.WinForms.Forms.BL
{
    class AddCategoryFormBL
    {
        private readonly ServiceClient _serviceClient;
        private readonly User _user;
        private readonly List<Category> _noteCategories;

        public AddCategoryFormBL(List<Category> noteCategories, ServiceClient client, User user)
        {
            _user = user;
            _serviceClient = client;
            _noteCategories = noteCategories;
        }

        public void CreateCategory(List<int> selectedCategoriesIndices, CheckedListBox listBoxCategories, 
            BindingList<Category> listCategories, string categoryName)
        {
            selectedCategoriesIndices.Clear();
            foreach (int index in listBoxCategories.CheckedIndices)
            {
                selectedCategoriesIndices.Add(index);
            }
            selectedCategoriesIndices.Add(listCategories.Count);
            listCategories.Add(_serviceClient.CreateCategory(categoryName, _user.Id));

            foreach (var index in selectedCategoriesIndices)
            {
                listBoxCategories.SetItemChecked(index, true);
            }
            var categories = _user.Categories.ToList();
            categories.Add(listCategories.Last());
            _user.Categories = categories;
        }

        public void UpdateCategories(BindingList<Category> categories)
        {
            categories.Clear();
            _user.Categories = _serviceClient.GetUserCategories(_user.Id);
            categories.AddRange(_user.Categories.Except(_noteCategories, new MainFormBL.CategoriesComparer()));
        }

        public void AddCategories(List<int> selectedCategoriesIndices, IList checkedIndices)
        {
            selectedCategoriesIndices.Clear();
            selectedCategoriesIndices.AddRange(checkedIndices.Cast<int>());
        }
    }
}
