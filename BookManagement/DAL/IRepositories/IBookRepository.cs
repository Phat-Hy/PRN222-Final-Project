using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepositories
{
    public interface IBookRepository
    {
        Task<BookSeries?> GetBookDetailsAsync(int id);
        Task<BookVolume?> GetVolumeDetailsAsync(int volumeId);
        Task<List<BookSeries>> GetAllBookSeriesAsync();
        Task<List<BookSeries>> SearchBookSeriesByNameAsync(string keyword);
        Task AddBookSeriesAsync(BookSeries series);
        Task UpdateBookSeriesAsync(BookSeries series);
        Task DeleteBookSeriesAsync(int id);
        Task AddBookVolumeAsync(BookVolume volume);
        Task UpdateBookVolumeAsync(BookVolume volume);
        Task DeleteBookVolumeAsync(int id);

    }
}
