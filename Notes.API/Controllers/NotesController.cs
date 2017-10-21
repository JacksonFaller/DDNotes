using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
            return _notesRepository.GetNotes();
        }

        /// <summary>
        /// Get note
        /// </summary>
        /// <param name="id">note id</param>
        /// <returns>returns note if exists</returns>
        [HttpGet]
        [Route("api/notes/{id}")]
        public Note Get(int id)
        {
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
            return _notesRepository.Create(note);
        }

        /// <summary>
        /// Update note
        /// </summary>
        /// <param name="note">note to update</param>
        /// <returns>updated note</returns>
        [HttpPut]
        [Route("api/notes")]
        public Note Update([FromBody]Note note)
        {
            return _notesRepository.Update(note);
        }

        /// <summary>
        /// Delete note
        /// </summary>
        /// <param name="id">note id to delete</param>
        [HttpDelete]
        [Route("api/notes/{id}")]
        public void Delete(int id)
        {
            _notesRepository.Delete(id);
        }
    }
}