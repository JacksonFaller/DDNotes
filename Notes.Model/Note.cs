using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Notes.Model
{
    public class Note
    {
        public int Id;
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime ChangingDate { get; set; }
        public DateTime CreationDate { get; set; }
        public int Creator { get; set; }
        public IEnumerable<User> Shared { get; set; }
        public IEnumerable<Category> Categories { get; set; }

        [IgnoreDataMember]
        public string CategoriesToString => string.Join(", ", Categories.Select(x => x.Name));
    }
}
