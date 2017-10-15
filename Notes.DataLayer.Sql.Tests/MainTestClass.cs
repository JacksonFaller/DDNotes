using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.DataLayer.Sql.Tests
{
    static class MainTestClass
    {
        public const string ConnectionString = 
            @"Data Source=JACKSONFALLERPC\SQLEXPRESS;Initial Catalog=NotesDB;Integrated Security=True";
        public const string UserName = "testuser";
        public const string UserPassword = "123";
        public const string NoteTitle = "NoteTitle";
        public const string NoteText = "NoteText";
        public const string CategoryName = "MyCategory";

        public static IEnumerable<string> GenerateCategories(int number)
        {
            StringBuilder sb = new StringBuilder(CategoryName);

            for (int i = 0; i < number; i++)
            {
                sb.Append(i.ToString());
                yield return sb.ToString();
                sb.Remove(CategoryName.Length, sb.Length - CategoryName.Length);
            }
        }
    }
}
