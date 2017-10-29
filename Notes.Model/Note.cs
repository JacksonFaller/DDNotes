using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Model
{
    public class Note
    {
        public int Id;
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime ChangingDate { get; set; }
        public DateTime CreationDate { get; set; }
        [Required]
        public int Creator { get; set; }
        public IEnumerable<User> Shared { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
