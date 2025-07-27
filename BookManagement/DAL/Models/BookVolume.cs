using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class BookVolume
    {
        public int Id { get; set; }

        public int VolumeNumber { get; set; }

        public int ChapterFrom { get; set; }
        public int ChapterTo { get; set; }

        public DateTime? ReleaseDate { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? Price { get; set; }

        public string CoverImageUrl { get; set; }

        public int BookSeriesId { get; set; }
        public BookSeries BookSeries { get; set; }
        public string Description { get; set; }
    }
}
