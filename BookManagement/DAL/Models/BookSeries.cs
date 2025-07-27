using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class BookSeries
    {
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string Name { get; set; }

        public string Author { get; set; }
        public string Illustrator { get; set; }
        public string? Translator { get; set; }

        public string CoverImageUrl { get; set; } // Default or representative image
        public string Description { get; set; }

        public int? CreatedByUserId { get; set; }
        public User? CreatedByUser { get; set; }

        public ICollection<BookVolume> Volumes { get; set; }
        public ICollection<UserBookCollection> UserCollections { get; set; }
    }

}
