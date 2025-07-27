using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class CollectionItemDto
    {
        public int BookSeriesId { get; set; }
        public string SeriesName { get; set; }
        public string CoverImageUrl { get; set; }
        public string OwnedVolumes { get; set; }
        public string WishlistVolumes { get; set; }
    }
}
