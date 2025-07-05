using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<UserBookCollection> UserBookCollections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User - UserBookCollection (1-to-many)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Collections)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Book - UserBookCollection (1-to-many)
            modelBuilder.Entity<Book>()
                .HasMany(b => b.UserCollections)
                .WithOne(uc => uc.Book)
                .HasForeignKey(uc => uc.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            // User - Book (user-created books)
            modelBuilder.Entity<Book>()
                .HasOne(b => b.CreatedByUser)
                .WithMany() // Optional: WithMany(u => u.CreatedBooks) if you want reverse navigation
                .HasForeignKey(b => b.CreatedByUserId)
                .OnDelete(DeleteBehavior.SetNull); // Keeps the book even if the user is deleted

            // Default timestamps
            modelBuilder.Entity<User>()
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<UserBookCollection>()
                .Property(uc => uc.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
