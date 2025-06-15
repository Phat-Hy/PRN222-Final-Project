using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string Name { get; set; }

        public int? Volume { get; set; }

        public string Author { get; set; }

        public string Illustrator { get; set; }

        public string Translator { get; set; }

        public int? Chapter { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public string CoverImageUrl { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? Price { get; set; }

        public ICollection<UserBookCollection> UserCollections { get; set; }
    }
}

