using DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Book
{
    public int Id { get; set; }

    [Required, MaxLength(255)]
    public string Name { get; set; }

    public int? Volume { get; set; }

    public string Author { get; set; }

    public string Illustrator { get; set; }

    public string? Translator { get; set; }

    public int? ChapterFrom { get; set; }
    public int? ChapterTo { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public string CoverImageUrl { get; set; }

    public string Description { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal? Price { get; set; }

    // Add this for user-created books
    public int? CreatedByUserId { get; set; }
    public User? CreatedByUser { get; set; }

    public ICollection<UserBookCollection> UserCollections { get; set; }
}
