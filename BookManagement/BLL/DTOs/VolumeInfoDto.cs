using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class VolumeInfoDto
    {
        public int Id { get; set; }  // VolumeId
        public int VolumeNumber { get; set; }
        public DateTime? ReleaseDate { get; set; }
    }
}
