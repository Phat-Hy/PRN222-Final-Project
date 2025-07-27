using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class BookVolumeDetailsDto
    {
        public int Id { get; set; }
        public int VolumeNumber { get; set; }
        public int ChapterFrom { get; set; }
        public int ChapterTo { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public decimal? Price { get; set; }
        public string CoverImageUrl { get; set; }
        public string Description { get; set; }

        public int BookSeriesId { get; set; }
        public string SeriesName { get; set; }
    }
}
