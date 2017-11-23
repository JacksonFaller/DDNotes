using System.ComponentModel;
using Notes.Model;

namespace Notes.WinForms.Forms.BL
{
    class EditSharedNoteFormBL
    {
        private readonly BindingList<Category> _categories;
        private bool _isNoteTextChanged;
        private bool _isNoteTitleChanged;

        public Note Note { get; }

        public EditSharedNoteFormBL(Note note, BindingList<Category> categories)
        {
            Note = note;
            _categories = categories;
            _categories.AddRange(note.Categories);
        }

        public void Save(string noteTitle, string noteText)
        {
            if (!_isNoteTitleChanged && !_isNoteTextChanged) return;
            Note.Title = _isNoteTitleChanged ? noteTitle : null;
            Note.Text = _isNoteTextChanged ? noteText : null;
        }

        public void SetNoteTitleChanged()
        {
            _isNoteTitleChanged = true;
        }

        public void SetNoteTextChanged()
        {
            _isNoteTextChanged = true;
        }
    }
}
