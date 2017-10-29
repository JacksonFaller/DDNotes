using System.ComponentModel.DataAnnotations;

namespace Notes.Model
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
