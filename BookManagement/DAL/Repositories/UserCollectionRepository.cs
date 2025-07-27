using DAL.IRepositories;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class UserCollectionRepository : IUserCollectionRepository
    {
        private readonly BookDbContext _context;

        public UserCollectionRepository(BookDbContext context)
        {
            _context = context;
        }

        public async Task<UserBookCollection?> GetByUserAndSeriesAsync(int userId, int seriesId)
        {
            return await _context.UserBookCollections
                .FirstOrDefaultAsync(c => c.UserId == userId && c.BookSeriesId == seriesId);
        }

        public async Task AddOrUpdateCollectionAsync(UserBookCollection collection)
        {
            var existing = await GetByUserAndSeriesAsync(collection.UserId, collection.BookSeriesId);
            if (existing != null)
            {
                existing.OwnedVolumes = collection.OwnedVolumes;
                existing.WishlistVolumes = collection.WishlistVolumes;
                _context.UserBookCollections.Update(existing);
            }
            else
            {
                await _context.UserBookCollections.AddAsync(collection);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<List<UserBookCollection>> GetByUserIdAsync(int userId)
        {
            return await _context.UserBookCollections
                .Include(c => c.BookSeries)
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }
        public async Task RemoveFromCollectionAsync(int userId, int bookSeriesId)
        {
            var collection = await GetByUserAndSeriesAsync(userId, bookSeriesId);
            if (collection != null)
            {
                _context.UserBookCollections.Remove(collection);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<UserBookCollection>> SearchUserCollectionsAsync(int userId, string keyword)
        {
            return await _context.UserBookCollections
                .Include(c => c.BookSeries)
                .Where(c => c.UserId == userId &&
                            c.BookSeries != null &&
                            c.BookSeries.Name.Contains(keyword))
                .ToListAsync();
        }

        public async Task<List<BookVolume>> GetUpcomingUnownedVolumesAsync(int userId)
        {
            var userCollections = await _context.UserBookCollections
                .Include(c => c.BookSeries)
                .ThenInclude(series => series.Volumes)
                .Where(c => c.UserId == userId)
                .ToListAsync();

            var result = new List<BookVolume>();

            foreach (var collection in userCollections)
            {
                var ownedVolumeNums = collection.OwnedVolumes
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(v => int.TryParse(v, out var n) ? n : -1)
                    .ToHashSet();

                var upcomingVolumes = collection.BookSeries.Volumes
                    .Where(v =>
                        v.ReleaseDate != null &&
                        v.ReleaseDate > DateTime.UtcNow &&
                        !ownedVolumeNums.Contains(v.VolumeNumber))
                    .ToList();

                result.AddRange(upcomingVolumes);
            }

            return result.OrderBy(v => v.ReleaseDate).ToList();
        }

    }

}
