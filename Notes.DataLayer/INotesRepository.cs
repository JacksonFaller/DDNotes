using System.Collections.Generic;
using Notes.Model;


namespace Notes.DataLayer
{
    public interface INotesRepository
    {
        Note Create(Note note);
        void Delete(int id);
        IEnumerable<Note> GetUsersNotes(int userId);
        Note Update(Note note);
        IEnumerable<Note> GetNotes();
        Note Get(int id);
    }
}
