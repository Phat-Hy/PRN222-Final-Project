using System;
using System.Collections.Generic;
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
        public DbSet<BookSeries> BookSeries { get; set; }
        public DbSet<BookVolume> BookVolumes { get; set; }
        public DbSet<UserBookCollection> UserBookCollections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // BookSeries - BookVolumes (1-to-many)
            modelBuilder.Entity<BookSeries>()
                .HasMany(bs => bs.Volumes)
                .WithOne(v => v.BookSeries)
                .HasForeignKey(v => v.BookSeriesId)
                .OnDelete(DeleteBehavior.Cascade);

            // BookSeries - UserBookCollection (1-to-many)
            modelBuilder.Entity<BookSeries>()
                .HasMany(bs => bs.UserCollections)
                .WithOne(uc => uc.BookSeries)
                .HasForeignKey(uc => uc.BookSeriesId)
                .OnDelete(DeleteBehavior.Cascade);

            // User - BookSeries (optional reverse navigation)
            modelBuilder.Entity<BookSeries>()
                .HasOne(bs => bs.CreatedByUser)
                .WithMany()
                .HasForeignKey(bs => bs.CreatedByUserId)
                .OnDelete(DeleteBehavior.SetNull);

            // User - UserBookCollection (1-to-many)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Collections)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

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
