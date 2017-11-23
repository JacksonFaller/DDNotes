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
    [ExceptionHandling]
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
            Logger.Logger.Instance.Info($"Получение категории с id: {id}.");
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
            Logger.Logger.Instance.Info(
                $"Создание категории с названием: {category.Name}, у пользователя {category.UserId}.");
            string errors = ModelStateValidator.Validate(ModelState);
            if (errors == null) return _categoriesRepository.Create(category.UserId, category.Name);
            Logger.Logger.Instance.Error(errors);
            throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));
        }

        /// <summary>
        /// Update category
        /// </summary>
        /// <param name="name">new category name</param>
        /// <param name="id">category id</param>
        [HttpPut]
        [Route("api/categories/{id}")]
        public Category Update([FromBody]string name, int id)
        {
            Logger.Logger.Instance.Info($"Изменение категории с id: {id}. Новое название: {name}.");
            if (name == null)
                throw new HttpResponseException(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent("Название заметки не может быть null.")
                });
            return _categoriesRepository.Update(name, id);
        }

        /// <summary>
        /// Delete category
        /// </summary>
        /// <param name="id">category id</param>
        [HttpDelete]
        [Route("api/categories/{id}")]
        public void Delete(int id)
        {
            Logger.Logger.Instance.Info($"Удаление категории с id: {id}.");
           _categoriesRepository.Delete(id);
        }
    }
}