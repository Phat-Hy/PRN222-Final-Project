using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class UpcomingVolumeDto
    {
        public int SeriesId { get; set; }
        public string SeriesName { get; set; }
        public string VolumeTitle => $"Vol. {VolumeNumber}";
        public int VolumeNumber { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string CoverImageUrl { get; set; }
    }
}
