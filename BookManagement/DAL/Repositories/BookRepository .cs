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
    public class BookRepository : IBookRepository
    {
        private readonly BookDbContext _context;

        public BookRepository(BookDbContext context)
        {
            _context = context;
        }

        public async Task<BookSeries?> GetBookDetailsAsync(int id)
        {
            return await _context.BookSeries
                .Include(bs => bs.Volumes)
                .FirstOrDefaultAsync(bs => bs.Id == id);
        }

        public async Task<BookVolume?> GetVolumeDetailsAsync(int volumeId)
        {
            return await _context.BookVolumes
                .Include(v => v.BookSeries)
                .FirstOrDefaultAsync(v => v.Id == volumeId);
        }

        public async Task<List<BookSeries>> GetAllBookSeriesAsync()
        {
            return await _context.BookSeries.ToListAsync();
        }

        public async Task<List<BookSeries>> SearchBookSeriesByNameAsync(string keyword)
        {
            return await _context.BookSeries
                .Where(bs => bs.Name.Contains(keyword))
                .ToListAsync();
        }

        public async Task AddBookSeriesAsync(BookSeries series)
        {
            await _context.BookSeries.AddAsync(series);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookSeriesAsync(BookSeries series)
        {
            _context.BookSeries.Update(series);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookSeriesAsync(int id)
        {
            var series = await _context.BookSeries.FindAsync(id);
            if (series != null)
            {
                _context.BookSeries.Remove(series);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddBookVolumeAsync(BookVolume volume)
        {
            await _context.BookVolumes.AddAsync(volume);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookVolumeAsync(BookVolume volume)
        {
            _context.BookVolumes.Update(volume);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookVolumeAsync(int id)
        {
            var volume = await _context.BookVolumes.FindAsync(id);
            if (volume != null)
            {
                _context.BookVolumes.Remove(volume);
                await _context.SaveChangesAsync();
            }
        }
    }
}
