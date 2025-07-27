using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class AddToCollectionDto
    {
        public int BookSeriesId { get; set; }
        public List<int> OwnedVolumes { get; set; } = new();
        public List<int> WishlistVolumes { get; set; } = new();
    }
}
