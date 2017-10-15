using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Notes.Model;
using static Notes.DataLayer.Sql.Tests.MainTestClass;

namespace Notes.DataLayer.Sql.Tests
{
    [TestClass]
    public class UsersRepositoryTests
    {
        private readonly UsersRepository _usersRepository;
        private bool _isCleanupNeeded;

        public UsersRepositoryTests()
        {
            var categoriesRepository = new CategoriesRepository(ConnectionString);
            var notesRepository = new NotesRepository(ConnectionString);
            _usersRepository = new UsersRepository(ConnectionString, categoriesRepository, notesRepository);
        }

        [TestMethod]
        public void CreateAndGetUserTest()
        {
            _isCleanupNeeded = true;
            
            // Create call Get method to return User
            var user = _usersRepository.Create(new User {Name = UserName, Password = UserPassword});

            Assert.AreEqual(UserName, user.Name);
        }

        [TestMethod]
        public void CreateAndDeleteUserTest()
        {
            _usersRepository.Create(new User { Name = UserName, Password = UserPassword });
            _usersRepository.Delete(UserName);

            try
            {
                _usersRepository.Get(UserName); 
                Assert.Fail("We found a user after deletion");
            }
            catch (ArgumentException e)
            {
                if (e.Message != $"Пользователь с именем: {UserName} не найден")
                    throw;
            }
        }

        [TestCleanup]
        public void CleanData()
        {
            if (_isCleanupNeeded)
            {
                _usersRepository.Delete(UserName);
                _isCleanupNeeded = false;
            }
        }
    }
}
