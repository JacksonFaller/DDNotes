using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        /// Get all notes
        /// </summary>
        /// <returns>returns notes enumeration</returns>
        [HttpGet]
        [Route("api/notes")]
        public IEnumerable<Note> GetNotes()
        {
            Logger.Logger.Instatnce.Info("Получение всех заметок.");
            return _notesRepository.GetNotes();
        }

        /// <summary>
        /// Get note
        /// </summary>
        /// <param name="id">note id</param>
        /// <returns>returns note if exists</returns>
        [HttpGet]
        [Route("api/notes/{id}")]
        [ArgumentExceptionFilter]
        public Note Get(int id)
        {
            Logger.Logger.Instatnce.Info($"Получение заметки с id: {id}.");
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
            Logger.Logger.Instatnce.Info(
                $"Создание заметки. Заголовок: {note.Title}, Текст: {note.Text}, Создатель: {note.Creator}.");
            string errors = ModelStateValidator.Validate(ModelState);
            if (errors == null) return _notesRepository.Create(note);
            Logger.Logger.Instatnce.Error(errors);
            throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));
        }

        /// <summary>
        /// Update note
        /// </summary>
        /// <param name="noteModel">note to update</param>
        /// <param name="id">note id</param>
        /// <returns>updated note</returns>
        [HttpPut]
        [Route("api/notes/{id}")]
        [ArgumentExceptionFilter]
        public Note Update([FromBody]UpdateNoteModel noteModel, int id)
        {
            Logger.Logger.Instatnce.Info(
                $"Изменение заметки с id: {id}. Новые - Заголовок: {noteModel.Title}, Текст: {noteModel.Text}.");
            Note note = noteModel;
            note.Id = id;
            return _notesRepository.Update(note);
        }

        /// <summary>
        /// Delete note
        /// </summary>
        /// <param name="id">note id to delete</param>
        [HttpDelete]
        [Route("api/notes/{id}")]
        [ArgumentExceptionFilter]
        public void Delete(int id)
        {
            Logger.Logger.Instatnce.Info($"Удалеие заметки с id: {id}.");
           _notesRepository.Delete(id);
        }
    }
}