using System.Collections.Generic;
using Notes.Model;


namespace Notes.DataLayer
{
    public interface INotesRepository
    {
        Note Create(Note note);
        void Delete(int id);
        IEnumerable<Note> GetUsersNotes(int userId);
        IEnumerable<User> GetSharedUsers(int noteId);
        IEnumerable<Note> GetSharedNotes(int userId);
        IEnumerable<Category> GetNoteCategories(int noteId);
        void AddCategory(int noteId, int categoryId);
        void RemoveCategory(int noteId, int categoryId);
        Note Update(Note note);
        Note Get(int id);
        void Share(int noteId, int userId);
        void Unshare(int noteId, int userId);
    }
}
