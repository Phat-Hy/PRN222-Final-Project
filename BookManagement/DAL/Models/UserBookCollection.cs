using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class UserBookCollection
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        public int BookId { get; set; }
        public Book Book { get; set; }

        public bool IsOwned { get; set; } = false;

        public bool IsWishlist { get; set; } = false;

        // e.g., "1,2,3" means user owns volumes 1, 2, and 3
        public string OwnedVolumes { get; set; }

        // e.g., "4,5" means user wants to buy volumes 4 and 5
        public string WishlistVolumes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
