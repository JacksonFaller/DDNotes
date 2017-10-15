using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Notes.Model;
using static Notes.DataLayer.Sql.Tests.MainTestClass;

namespace Notes.DataLayer.Sql.Tests
{
    /// <summary>
    /// Summary description for CategoriesRepositoryTests
    /// </summary>
    [TestClass]
    public class CategoriesRepositoryTests
    {
        private readonly CategoriesRepository _categoriesRepository = new CategoriesRepository(ConnectionString);
        private readonly UsersRepository _usersRepository;
        
        public CategoriesRepositoryTests()
        {
            var notesRepository = new NotesRepository(ConnectionString);
            _usersRepository = new UsersRepository(ConnectionString, _categoriesRepository, notesRepository);
        }

        [TestMethod]
        public void CreateCategoryTest()
        {
            // Create user to add category
            var user = _usersRepository.Create(new User() {Name = UserName, Password = UserPassword});
            var result =_categoriesRepository.Create(user.Id, CategoryName);

            Assert.AreEqual(CategoryName, result.Name);
        }

        [TestMethod]
        public void GetCategoryTest()
        {
            // Create user to add category
            var user = _usersRepository.Create(new User() { Name = UserName, Password = UserPassword });
            _categoriesRepository.Create(user.Id, CategoryName);

            var result = _categoriesRepository.Get(user.Id, CategoryName);

            Assert.AreEqual(CategoryName, result.Name);
        }

        [TestMethod]
        public void CreateAndDeleteCategoryTest()
        {
            // Create user to add category
            var user = _usersRepository.Create(new User() { Name = UserName, Password = UserPassword });
            var result = _categoriesRepository.Create(user.Id, CategoryName);
            _categoriesRepository.Delete(result.Id);

            try
            {
                _categoriesRepository.Get(user.Id, CategoryName);
                Assert.Fail("We found a category after deletion");
            }
            catch (ArgumentException e)
            {
                if (e.Message != $"Категория с именем: {CategoryName} не найдена")
                {
                    throw;
                }
            }
        }

        [TestMethod]
        public void GetUsersCategoriesTest()
        {
            // Create user to add category
            var user = _usersRepository.Create(new User() { Name = UserName, Password = UserPassword });
            var categoriesNames = GenerateCategories(new Random().Next(1, 10)).ToList();

            foreach (string categoryName in categoriesNames)
            {
                _categoriesRepository.Create(user.Id, categoryName);
            }

            var categories = _categoriesRepository.GetUsersCategories(user.Id).ToList();


            Console.WriteLine("Found categories:");
            if (categories.Count != categoriesNames.Count)
            {
                foreach (Category category in categories)
                {
                    Console.WriteLine(category.Name);
                }
                Assert.Fail($"Expected {categoriesNames.Count} categories, but we found {categories.Count}");
            }
            foreach (string categoryName in categoriesNames)
            {
                bool isFound = false;
                foreach (Category category in categories)
                {
                    if (category.Name == categoryName)
                    {
                        isFound = true;
                        Console.WriteLine(category.Name);
                        break;
                    }
                }
                if(!isFound) Assert.Fail($"Category {categoryName} not found");
            }
        }

        [TestCleanup]
        public void CleanData()
        {
            // All users categories will be deleted due to cascade delition rule in database
            _usersRepository.Delete(UserName);
        }
    }
}
