using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IServices
{
    public interface IBookCollectionService
    {
        Task AddToCollectionAsync(int userId, AddToCollectionDto dto);
        Task<List<CollectionItemDto>> GetUserCollectionAsync(int userId);
        Task UpdateCollectionAsync(int userId, UpdateCollectionDto dto);
        Task RemoveFromCollectionAsync(int userId, int bookSeriesId);
        Task<List<CollectionItemDto>> SearchUserCollectionAsync(int userId, string keyword);
        Task<List<UpcomingVolumeDto>> GetUpcomingUnownedVolumesAsync(int userId);

    }
}
