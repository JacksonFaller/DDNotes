using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections.Generic;
using Notes.DataLayer;
using Notes.DataLayer.Sql;
using Notes.Model;

namespace Notes.API.Controllers
{
    /// <summary>
    /// UsersRepository controller
    /// </summary>
    [ExceptionHandling]
    public class UsersController : ApiController
    {
        private const string ConnectionString =
            @"Data Source=JACKSONFALLERPC\SQLEXPRESS;Initial Catalog=NotesDB;Integrated Security=True";

        private readonly IUsersRepository _usersRepository;
        private readonly INotesRepository _notesRepository;
        private readonly ICategoriesRepository _categoriesRepository;

        public UsersController()
        {
            _notesRepository = new NotesRepository(ConnectionString);
            _categoriesRepository = new CategoriesRepository(ConnectionString);
            _usersRepository = new UsersRepository(ConnectionString, _categoriesRepository, _notesRepository);
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>users enumeration</returns>
        [HttpGet]
        [Route("api/users")]
        public IEnumerable<User> GetUsers()
        {
            Logger.Logger.Instance.Info("Получение всех пользователей.");
            return _usersRepository.GetUsers();
        }

        /// <summary>
        /// Get user
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>returns user if exists</returns>
        [HttpGet]
        [Route("api/users/{id}")]
        public User Get(int id)
        {
            Logger.Logger.Instance.Info($"Получение пользователя с id: {id}.");
            return _usersRepository.Get(id);
        }

        /// <summary>
        /// Get user by name
        /// </summary>
        /// <param name="name">user name</param>
        /// <returns>returns user if exists</returns>
        [HttpGet]
        [Route("api/users/byName/{name}")]
        public User Get(string name)
        {
            Logger.Logger.Instance.Info($"Получение пользователя с именем: {name}.");
            return _usersRepository.Get(name);
        }

        /// <summary>
        /// Get users categories
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>users categories enumeration</returns>
        [HttpGet]
        [Route("api/users/{id}/categories")]
        public IEnumerable<Category> GetCategories(int id)
        {
            Logger.Logger.Instance.Info($"Получение всех категорий пользователя с id: {id}.");
            return _categoriesRepository.GetCategories(id);
        }

        /// <summary>
        /// Get users notes
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>users notes enumeration</returns>
        [HttpGet]
        [Route("api/users/{id}/notes")]
        public IEnumerable<Note> GetNotes(int id)
        {
            Logger.Logger.Instance.Info($"Получение всех заметок пользователя с id: {id}.");
            return _notesRepository.GetUserNotes(id);
        }

        /// <summary>
        /// Get users shared notes
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>shared users notes enumeration</returns>
        [HttpGet]
        [Route("api/users/{id}/sharedNotes")]
        public IEnumerable<Note> GetSharedNotes(int id)
        {
            Logger.Logger.Instance.Info($"Получение всех доступных заметок пользователю с id: {id}.");
            return _notesRepository.GetSharedNotes(id);
        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="user">user to create</param>
        /// <returns>returns new user</returns>
        [HttpPost]
        [Route("api/users")]
        public User Create([FromBody]User user)
        {
            Logger.Logger.Instance.Info($"Создание пользователя с Именем: {user.Name} и Паролем: {user.Password}.");
            // Проверка валидности модели
            string errors = ModelStateValidator.Validate(ModelState);
            if (errors == null) return _usersRepository.Create(user);
            Logger.Logger.Instance.Error(errors);
            throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));
        }

        /// <summary>
        /// Validate user
        /// </summary>
        /// <param name="user">user to validate</param>
        /// <returns>true if user valid, otherwise false</returns>
        [HttpPost]
        [Route("api/users/validate")]
        public User Validate([FromBody]User user)
        {
            Logger.Logger.Instance.Info($"Валидация пользователя с именем: {user.Name} и паролем: {user.Password}.");
            var resultUser = Get(user.Name);
            if (user.Password.Equals(resultUser.Password, StringComparison.Ordinal))
                return resultUser;
            var response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
            {
                Content = new StringContent("Неверное имя пользователя или пароль")
            };
            throw new HttpResponseException(response);

        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="id">user id</param>
        [HttpDelete]
        [Route("api/users/{id}")]
        public void Delete(int id)
        {
            Logger.Logger.Instance.Info($"Удаление пользователя с id: {id}.");
            _usersRepository.Delete(id);
        }
    }
}