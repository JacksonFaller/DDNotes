using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Web.Http;
using Notes.DataLayer;
using Notes.DataLayer.Sql;
using Notes.Model;
using Notes.API.Filters;

namespace Notes.API.Controllers
{
    /// <summary>
    /// UsersRepository controller
    /// </summary>
    public class UsersController : ApiController
    {
        private const string ConnectionString =
            @"Data Source=JACKSONFALLERPC\SQLEXPRESS;Initial Catalog=NotesDB;Integrated Security=True";

        private readonly IUsersRepository _usersRepository;

        public UsersController()
        {
            _usersRepository = new UsersRepository(
                ConnectionString, new CategoriesRepository(ConnectionString), new NotesRepository(ConnectionString));
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>users enumeration</returns>
        [HttpGet]
        [Route("api/users")]
        public IEnumerable<User> GetUsers()
        {
            Logger.Logger.Instatnce.Info("Получение всех пользователей.");
            return _usersRepository.GetUsers();
        }

        /// <summary>
        /// Get user
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>returns user if exists</returns>
        [HttpGet]
        [Route("api/users/{id}")]
        [ArgumentExceptionFilter]
        public User Get(int id)
        {
            Logger.Logger.Instatnce.Info($"Получение пользователя с id: {id}.");
            return _usersRepository.Get(id);

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
            Logger.Logger.Instatnce.Info($"Получение всех категорий пользователя с id: {id}.");
            return _usersRepository.GetCategories(id);
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
            Logger.Logger.Instatnce.Info($"Создание пользователя с Именем: {user.Name} и Паролем: {user.Password?.GetHashCode()}.");
            // Проверка валидности модели
            string errors = ModelStateValidator.Validate(ModelState);
            if (errors == null) return _usersRepository.Create(user);
            Logger.Logger.Instatnce.Error(errors);
            throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="id">user id</param>
        [HttpDelete]
        [Route("api/users/{id}")]
        public void Delete(int id)
        {
            Logger.Logger.Instatnce.Info($"Удаление пользователя с id: {id}.");
            _usersRepository.Delete(id);
        }
    }
}