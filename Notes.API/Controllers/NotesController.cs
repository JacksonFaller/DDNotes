using System.Collections.Generic;
using System.Web.Http;
using Notes.API.Filters;
using Notes.DataLayer;
using Notes.DataLayer.Sql;
using Notes.Model;

namespace Notes.API.Controllers
{
    /// <summary>
    /// NotesRepository controller
    /// </summary>
    public class NotesController : ApiController
    {
        private const string ConnectionString =
            @"Data Source=JACKSONFALLERPC\SQLEXPRESS;Initial Catalog=NotesDB;Integrated Security=True";

        private readonly INotesRepository _notesRepository;

        public NotesController()
        {
            _notesRepository = new NotesRepository(ConnectionString);
        }

        /// <summary>
        /// Get note
        /// </summary>
        /// <param name="id">note id</param>
        /// <returns>returns note if exists</returns>
        [HttpGet]
        [Route("api/notes/{id}")]
        [ExceptionHandling]
        public Note Get(int id)
        {
            Logger.Logger.Instance.Info($"Получение заметки с id: {id}.");
            return _notesRepository.Get(id);
        }

        /// <summary>
        /// Create new note
        /// </summary>
        /// <param name="note">note to create</param>
        /// <returns>returns new note</returns>
        [HttpPost]
        [Route("api/notes")]
        public Note Create([FromBody]Note note)
        {
            Logger.Logger.Instance.Info(
                $"Создание заметки. Заголовок: {note.Title}, Текст: {note.Text}, Создатель: {note.Creator}.");
            return _notesRepository.Create(note);
            //throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));
        }

        /// <summary>
        /// Update note
        /// </summary>
        /// <param name="note">note to update</param>
        /// <param name="id">note id</param>
        /// <returns>updated note</returns>
        [HttpPut]
        [Route("api/notes/{id}")]
        [ExceptionHandling]
        public Note Update([FromBody]Note note, int id)
        {
            Logger.Logger.Instance.Info(
                $"Изменение заметки с id: {id}. Новые - Заголовок: {note.Title}, Текст: {note.Text}.");
            note.Id = id;
            return _notesRepository.Update(note);
        }

        /// <summary>
        /// Delete note
        /// </summary>
        /// <param name="id">note id to delete</param>
        [HttpDelete]
        [Route("api/notes/{id}")]
        [ExceptionHandling]
        public void Delete(int id)
        {
            Logger.Logger.Instance.Info($"Удалеие заметки с id: {id}.");
           _notesRepository.Delete(id);
        }

        /// <summary>
        /// Share note
        /// </summary>
        /// <param name="id">note id to share</param>
        /// <param name="userId">shared user id</param>
        [HttpPost]
        [Route("api/notes/{id}/share/{userId}")]
        [ExceptionHandling]
        public void Share(int id, int userId)
        {
            Logger.Logger.Instance.Info($"Поделиться заметкой с id: {id} с пользователем {userId}");
            _notesRepository.Share(id, userId);
        }

        /// <summary>
        /// Unshare note
        /// </summary>
        /// <param name="id">note id to unshare</param>
        /// <param name="userId">unshared user id</param>
        [HttpDelete]
        [Route("api/notes/{id}/unshare/{userId}")]
        [ExceptionHandling]
        public void Unshare(int id, int userId)
        {
            Logger.Logger.Instance.Info($"Скрыть заметку заметку с id: {id} от пользователя {userId}");
            _notesRepository.Unshare(id, userId);
        }

        /// <summary>
        /// Get shared users
        /// </summary>
        /// <param name="id">note id</param>
        /// <returns>enumeration of shared users</returns>
        [HttpGet]
        [Route("api/notes/{id}/sharedUsers")]
        [ExceptionHandling]
        public IEnumerable<User> GetSharedUsers(int id)
        {
            Logger.Logger.Instance.Info($"Получение общих пользователей заметки с id: {id}.");
            return _notesRepository.GetSharedUsers(id);
        }

        /// <summary>
        /// Add category to note
        /// </summary>
        /// <param name="categoryId">category id to add to note</param>
        /// <param name="noteId">note id</param>
        [HttpPut]
        [Route("api/notes/{noteId}/addCategory")]
        [ExceptionHandling]
        public void AddCategory([FromBody]int categoryId, int noteId)
        {
            Logger.Logger.Instance.Info(
                $"Добавлении категории с id: {categoryId} к заметке с id: {noteId}.");
            _notesRepository.AddCategory(noteId, categoryId);
        }

        /// <summary>
        /// Remove category from note
        /// </summary>
        /// <param name="categoryId">category id to remove from note</param>
        /// <param name="noteId">note id</param>
        [HttpPut]
        [Route("api/notes/{noteId}/removeCategory")]
        [ExceptionHandling]
        public void RemoveCategory([FromBody]int categoryId, int noteId)
        {
            Logger.Logger.Instance.Info(
                $"Добавлении категории с id: {categoryId} к заметке с id: {noteId}.");
            _notesRepository.RemoveCategory(noteId, categoryId);
        }

        /// <summary>
        /// Get note categories
        /// </summary>
        /// <param name="id">note id</param>
        /// <returns>enumeration of note categories</returns>
        [HttpGet]
        [Route("api/notes/{id}/categories")]
        [ExceptionHandling]
        public IEnumerable<Category> GetNoteCategories(int id)
        {
            Logger.Logger.Instance.Info($"Получение категорий заметки с id: {id}.");
            return _notesRepository.GetNoteCategories(id);
        }
    }
}