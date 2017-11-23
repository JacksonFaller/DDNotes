using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Notes.Model;

namespace Notes.WinForms.Forms.BL
{
    class CategoriesFilterFormBL
    {
        private readonly ServiceClient _serviceClient;
        private readonly int _userId;
        public readonly BindingList<Category> Categories = new BindingList<Category>();
        public List<int> SelectedCategoriestIndices = new List<int>();

        public CategoriesFilterFormBL(ServiceClient client, int userId, IEnumerable<Category> categories, 
            IEnumerable<int> filteredCategoriesIndices)
        {
            _serviceClient = client;
            _userId = userId;
            Categories.AddRange(categories);
            SelectedCategoriestIndices.AddRange(filteredCategoriesIndices);
        }

        public void UpdateCategories()
        {
            Categories.AddRange(_serviceClient.GetUserCategories(_userId));
        }

        public void Filter(IEnumerable<int> indices)
        {
            SelectedCategoriestIndices.Clear();
            SelectedCategoriestIndices.AddRange(indices);
        }
    }
}
