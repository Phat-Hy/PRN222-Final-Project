using BLL.DTOs;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IServices
{
    public interface IBookService
    {
        Task<BookDetailsDto?> GetBookDetailsAsync(int id);
        Task<BookVolumeDetailsDto?> GetVolumeDetailsAsync(int volumeId);
        Task<List<BookSeriesListDto>> GetAllBookSeriesAsync();
        Task<List<BookSeriesListDto>> SearchBookSeriesByNameAsync(string keyword);
        Task AddBookSeriesAsync(BookSeries series);
        Task UpdateBookSeriesAsync(BookSeries series);
        Task DeleteBookSeriesAsync(int id);
        Task AddBookVolumeAsync(BookVolume volume);
        Task UpdateBookVolumeAsync(BookVolume volume);
        Task DeleteBookVolumeAsync(int id);

    }
}
