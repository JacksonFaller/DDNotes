using System.Collections.Generic;

namespace Notes.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Note> Notes { get; set; }
    }
}
