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
    /// CategoriesRepository controller
    /// </summary>
    public class CategoriesController : ApiController
    {
        private const string ConnectionString =
            @"Data Source=JACKSONFALLERPC\SQLEXPRESS;Initial Catalog=NotesDB;Integrated Security=True";

        private readonly ICategoriesRepository _categoriesRepository;

        public CategoriesController()
        {
            _categoriesRepository = new CategoriesRepository(ConnectionString);
        }

        /// <summary>
        /// Get category by its id
        /// </summary>
        /// <param name="id">category id</param>
        /// <returns>returns category if exists</returns>
        [HttpGet]
        [Route("api/categories/{id}")]
        public Category Get(int id)
        {
            return _categoriesRepository.Get(id);
        }

        /// <summary>
        /// Create new category
        /// </summary>
        /// <param name="category">category to create</param>
        /// <returns>returns new category</returns>
        [HttpPost]
        [Route("api/categories")]
        public Category Create([FromBody]Category category)
        {
            return _categoriesRepository.Create(category.UserId, category.Name);
        }

        /// <summary>
        /// Update category
        /// </summary>
        /// <param name="name">new category name</param>
        /// <param name="id">category id</param>
        [HttpPut]
        [Route("api/categories/{id}")]
        public void Update([FromBody]string name, int id)
        {
            _categoriesRepository.Update(name, id);
        }

        /// <summary>
        /// Delete category
        /// </summary>
        /// <param name="id">category id</param>
        [HttpDelete]
        [Route("api/categories/{id}")]
        public void Delete(int id)
        {
            _categoriesRepository.Delete(id);
        }
    }
}