using Notes.Model;

namespace Notes.API.Models
{
    /// <summary>
    /// Модель изменения заметки
    /// </summary>
    public class UpdateNoteModel
    {
        public string Title { get; set; }
        public string Text { get; set; }

        /// <summary>
        /// Приведение модели изменения к базовой модели заметки
        /// </summary>
        /// <param name="noteModel">модель изменения заметки</param>
        public static implicit operator Note(UpdateNoteModel noteModel)
        {
            return new Note
            {
                Title = noteModel.Title,
                Text = noteModel.Text
            };
        }
    }
}