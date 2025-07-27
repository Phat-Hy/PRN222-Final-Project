using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class UserBookCollectionDto
    {
        public int SeriesId { get; set; }
        public string SeriesName { get; set; }
        public string CoverImageUrl { get; set; }

        public List<int> OwnedVolumes { get; set; } = new();
        public List<int> WishlistVolumes { get; set; } = new();
    }
}
