using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Notes.Model
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Note> Notes { get; set; }
    }
}
