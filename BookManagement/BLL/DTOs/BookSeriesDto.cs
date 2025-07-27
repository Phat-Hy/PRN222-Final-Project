using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class BookSeriesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Illustrator { get; set; }
        public string? Translator { get; set; }
        public string CoverImageUrl { get; set; }
        public string Description { get; set; }
    }
}
