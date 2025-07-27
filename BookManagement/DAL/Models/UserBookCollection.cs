using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class UserBookCollection
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        public int BookSeriesId { get; set; }
        public BookSeries BookSeries { get; set; }

        /// <summary>
        /// Whether the user owns at least one volume in the series
        /// </summary>
        public bool IsOwned => !string.IsNullOrEmpty(OwnedVolumes);

        /// <summary>
        /// Whether the user wishes to own at least one volume in the series
        /// </summary>
        public bool IsWishlist => !string.IsNullOrEmpty(WishlistVolumes);

        /// <summary>
        /// Comma-separated string of owned volume numbers (e.g., "1,2,3")
        /// </summary>
        public string OwnedVolumes { get; set; } = string.Empty;

        /// <summary>
        /// Comma-separated string of wishlist volume numbers (e.g., "4,5")
        /// </summary>
        public string WishlistVolumes { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
