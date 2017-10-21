using Notes.DataLayer;
using Notes.DataLayer.Sql;
using Notes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

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
            return _usersRepository.Create(user);
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="id">user id</param>
        [HttpDelete]
        [Route("api/users/{id}")]
        public void Delete(int id)
        {
            _usersRepository.Delete(id);
        }
    }
}