using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepositories
{
    public interface IUserCollectionRepository
    {
        Task<UserBookCollection?> GetByUserAndSeriesAsync(int userId, int seriesId);
        Task AddOrUpdateCollectionAsync(UserBookCollection collection);
        Task<List<UserBookCollection>> GetByUserIdAsync(int userId);
        Task RemoveFromCollectionAsync(int userId, int bookSeriesId);
        Task<List<UserBookCollection>> SearchUserCollectionsAsync(int userId, string keyword);
        Task<List<BookVolume>> GetUpcomingUnownedVolumesAsync(int userId);

    }
}
