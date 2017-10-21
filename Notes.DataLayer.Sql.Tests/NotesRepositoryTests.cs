using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Notes.Model;
using static Notes.DataLayer.Sql.Tests.MainTestClass;

namespace Notes.DataLayer.Sql.Tests
{
    [TestClass]
    public class NotesRepositoryTests
    {
        private readonly UsersRepository _usersRepository;
        private readonly CategoriesRepository _categoriesRepository = new CategoriesRepository(ConnectionString);
        private readonly NotesRepository _notesRepository;

        private List<string> Users = new List<string>();

        public NotesRepositoryTests()
        {
            _notesRepository = new NotesRepository(ConnectionString);
            _usersRepository = new UsersRepository(ConnectionString, _categoriesRepository, _notesRepository);
        }

        [TestMethod]
        public void CreateNoteTest()
        {
            Users.Add(UserName);
            User user = _usersRepository.Create(new User {Name = UserName, Password = UserPassword});
            Note note = _notesRepository.Create(new Note {Title = NoteTitle, Text = NoteText, Creator = user.Id});

            Assert.AreEqual(NoteTitle, note.Title);
        }

        [TestMethod]
        public void GetAndDeleteNoteTest()
        {
            Users.Add(UserName);
            User user = _usersRepository.Create(new User { Name = UserName, Password = UserPassword });
            Note note = _notesRepository.Create(new Note { Title = NoteTitle, Text = NoteText, Creator = user.Id });
            _notesRepository.Delete(note.Id);

            try
            {
                _notesRepository.Get(note.Id);
                Assert.Fail();
            }
            catch (ArgumentException e)
            {
                if (e.Message != $"Заметка с id: {note.Id} не найдена")
                {
                    throw;
                }
            }

        }

        [TestMethod]
        public void GetUsersNotesTest()
        {
            Users.Add(UserName);
            User user = _usersRepository.Create(new User { Name = UserName, Password = UserPassword });
            
            Random random = new Random();
            int notesNum = random.Next(1, 5);
            List<int> notesId = new List<int>();

            for (int i = 0; i < notesNum; i++)
            {
                var note = _notesRepository.Create(new Note {Title = NoteTitle, Text = NoteText, Creator = user.Id});
                notesId.Add(note.Id);
            }

            var usersNotes = _notesRepository.GetUsersNotes(user.Id).ToList();

            Console.WriteLine("Found note ids:");
            if (usersNotes.Count != notesId.Count)
            {
                foreach (Note note in usersNotes)
                {
                    Console.WriteLine(note.Id);
                }
                Assert.Fail($"Expected {notesId.Count} notes, but we found {usersNotes.Count}");
            }
            foreach (int noteId in notesId)
            {
                bool isFound = false;
                foreach (Note note in usersNotes)
                {
                    if (note.Id == noteId)
                    {
                        usersNotes.Remove(note);
                        isFound = true;
                        Console.WriteLine(note.Id);
                        break;
                    }
                }
                if (!isFound) Assert.Fail($"Note with id: {noteId} not found");
            }

        }

        [TestMethod]
        public void GetSharedNotesTest()
        {
            const string sharedUserName = "sharedUser";
            Users.Add(sharedUserName);
            Users.Add(UserName);

            User user = _usersRepository.Create(new User { Name = UserName, Password = UserPassword });
            User sharedUser = _usersRepository.Create(new User { Name = sharedUserName, Password = UserPassword });
            Note note = _notesRepository.Create(new Note { Title = NoteTitle, Text = NoteText, Creator = user.Id });
            _notesRepository.Share(note.Id, sharedUser.Id);
            var sharedNotes = _notesRepository.GetSharedNotes(sharedUser.Id);

            Console.WriteLine("Found shared note ids:");
            foreach (Note sharedNote in sharedNotes)
            {
                Console.WriteLine(sharedNote.Id);
                if(sharedNote.Id == note.Id) return;
            }
            Assert.Fail($"Expected to found shared note {note.Id} to user {sharedUser.Id}.");
        }

        [TestMethod]
        public void ShareAndGetSharedUsersTest()
        {
            const string sharedUserName = "sharedUser";
            Users.Add(sharedUserName);
            Users.Add(UserName);

            User user = _usersRepository.Create(new User { Name = UserName, Password = UserPassword });
            User sharedUser = _usersRepository.Create(new User { Name = sharedUserName, Password = UserPassword });
            Note note = _notesRepository.Create(new Note { Title = NoteTitle, Text = NoteText, Creator = user.Id });
            _notesRepository.Share(note.Id, sharedUser.Id);
            var sharedUsers = _notesRepository.GetSharedUsers(note.Id);

            Console.WriteLine("Found notes shared user ids:");
            foreach (User sharedUsr in sharedUsers)
            {
                Console.WriteLine(sharedUsr.Id);
                if(sharedUsr.Id == sharedUser.Id) return;
            }
            Assert.Fail($"Expected to found user {sharedUser.Id}.");
        }

        [TestMethod]
        public void UnShareTest()
        {
            const string sharedUserName = "sharedUser";
            Users.Add(sharedUserName);
            Users.Add(UserName);

            User user = _usersRepository.Create(new User { Name = UserName, Password = UserPassword });
            User sharedUser = _usersRepository.Create(new User { Name = sharedUserName, Password = UserPassword });
            Note note = _notesRepository.Create(new Note { Title = NoteTitle, Text = NoteText, Creator = user.Id });
            _notesRepository.Share(note.Id, sharedUser.Id);
            _notesRepository.UnShare(note.Id, sharedUser.Id);
            var result = _notesRepository.GetSharedNotes(sharedUser.Id);

            Console.WriteLine($"Shared to user {sharedUser.Id} note ids:");
            foreach (Note sharedNote in result)
            {
                Console.WriteLine(sharedNote.Id);
                if(sharedNote.Id == note.Id) 
                    Assert.Fail($"Unexpected shared note {note.Id} to user {sharedUser.Id} after UnShare.");
            }
        }

        [TestMethod]
        public void GetNoteCategoriesTest()
        {
            Users.Add(UserName);
            User user = _usersRepository.Create(new User { Name = UserName, Password = UserPassword });
            Note note = _notesRepository.Create(new Note { Title = NoteTitle, Text = NoteText, Creator = user.Id });
            Category category = _categoriesRepository.Create(user.Id, CategoryName);
            _notesRepository.AddCategory(note, category);

            var result = _notesRepository.GetNoteCategories(note.Id);

            Console.WriteLine("Found note category ids:");
            foreach (var noteCategory in result)
            {
                Console.WriteLine(noteCategory.Id);
                if (noteCategory.Id == category.Id) return;
            }
            Assert.Fail($"Expected to found category {category.Id}");
        }

        [TestMethod]
        public void AddAndRemoveCategoryTest()
        {
            Users.Add(UserName);
            User user = _usersRepository.Create(new User { Name = UserName, Password = UserPassword });
            Note note = _notesRepository.Create(new Note { Title = NoteTitle, Text = NoteText, Creator = user.Id });
            Category category = _categoriesRepository.Create(user.Id, CategoryName);
            _notesRepository.AddCategory(note, category);
            _notesRepository.RemoveCategory(note, category);

            var result = _notesRepository.GetNoteCategories(note.Id);

            foreach (Category noteCategory in result)
            {
                if(noteCategory.Id == category.Id)
                    Assert.Fail($"Unexpected category {category.Id} in note {note.Id} after remove.");
            }
        }

        [TestMethod]
        public void UpdateNoteTest()
        {
            const string newText = "NewText";
            const string newTitle= "NewTitle";
            Users.Add(UserName);

            User user = _usersRepository.Create(new User { Name = UserName, Password = UserPassword });
            Note note = _notesRepository.Create(new Note { Title = NoteTitle, Text = NoteText, Creator = user.Id });
            note.Text = newText;
            note.Title = newTitle;

            _notesRepository.Update(note);
            var result = _notesRepository.Get(note.Id);

            Console.WriteLine($"Updated note NoteText: {result.Text}, NoteTitle: {result.Title}");
            if (result.Text != note.Text || result.Title != note.Title)
                Assert.Fail($"Expected to get note with NoteText: {note.Text}, NoteTitle: {note.Title}");
        }

        [TestCleanup]
        public void CleanData()
        {
            // All users Notes will be deleted due to cascade delition rule in database
            foreach (string userName in Users)
            {
                _usersRepository.Delete(userName);
            }
            Users.Clear();
        }
    }
}
