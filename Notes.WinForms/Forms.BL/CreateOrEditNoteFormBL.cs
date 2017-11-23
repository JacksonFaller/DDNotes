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
    class CreateOrEditNoteFormBL
    {
        public Note Note { get; set; }
        public List<Category> AddedCategories = new List<Category>();
        public List<Category> RemovedCategories = new List<Category>();

        public bool IsNoteTextChanged;
        public bool IsNoteTitleChanged;

        private readonly ServiceClient _serviceClient;
        private readonly User _user;
        private readonly BindingList<Category> _categories;

        public CreateOrEditNoteFormBL(ServiceClient client, User user, Note note, BindingList<Category> categories)
        {
            _serviceClient = client;
            _user = user;
            Note = note;
            _categories = categories;

            if (note.Categories == null) return;
            _categories.AddRange(note.Categories);
        }

        public void SaveNote(string noteTitle, string noteText)
        {
            if (!IsNoteTitleChanged && !IsNoteTextChanged) return;
            Note.Title = IsNoteTitleChanged ? noteTitle : null;
            Note.Text = IsNoteTextChanged ? noteText : null;
            Note.Categories = _categories;
        }

        public void AddCategory()
        {
            using (var form = new AddCategoryForm(_categories, _serviceClient, _user))
            {
                if (form.ShowDialog() != DialogResult.OK || form.SelectedCategoriesIndices.Count == 0) return;
                foreach (var index in form.SelectedCategoriesIndices)
                {
                    _categories.Add(form.Categories[index]);
                    AddedCategories.Add(form.Categories[index]);
                }
            }
        }

        public void DeleteCategory(int index)
        {
            if (_categories.Count == 0) return;
            if (AddedCategories.Contains(_categories[index]))
            {
                AddedCategories.Remove(_categories[index]);
                _categories.RemoveAt(index);
                return;
            }
            RemovedCategories.Add(_categories[index]);
            _categories.RemoveAt(index);
        }

    }
}
