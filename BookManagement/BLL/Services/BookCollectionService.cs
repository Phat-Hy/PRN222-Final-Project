using BLL.DTOs;
using BLL.IServices;
using DAL.IRepositories;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class BookCollectionService : IBookCollectionService
    {
        private readonly IUserCollectionRepository _collectionRepo;

        public BookCollectionService(IUserCollectionRepository collectionRepo)
        {
            _collectionRepo = collectionRepo;
        }

        public async Task AddToCollectionAsync(int userId, AddToCollectionDto dto)
        {
            var collection = new UserBookCollection
            {
                UserId = userId,
                BookSeriesId = dto.BookSeriesId,
                OwnedVolumes = string.Join(",", dto.OwnedVolumes.Distinct()),
                WishlistVolumes = string.Join(",", dto.WishlistVolumes.Distinct())
            };

            await _collectionRepo.AddOrUpdateCollectionAsync(collection);
        }
        public async Task<List<CollectionItemDto>> GetUserCollectionAsync(int userId)
        {
            var collections = await _collectionRepo.GetByUserIdAsync(userId);
            return collections.Select(c => new CollectionItemDto
            {
                BookSeriesId = c.BookSeriesId,
                SeriesName = c.BookSeries?.Name ?? "",
                CoverImageUrl = c.BookSeries?.CoverImageUrl ?? "",
                OwnedVolumes = c.OwnedVolumes,
                WishlistVolumes = c.WishlistVolumes
            }).ToList();
        }
        public async Task UpdateCollectionAsync(int userId, UpdateCollectionDto dto)
        {
            var existing = await _collectionRepo.GetByUserAndSeriesAsync(userId, dto.BookSeriesId);
            if (existing == null)
                throw new Exception("Collection not found."); // or handle more gracefully

            existing.OwnedVolumes = string.Join(",", dto.OwnedVolumes.Distinct());
            existing.WishlistVolumes = string.Join(",", dto.WishlistVolumes.Distinct());

            await _collectionRepo.AddOrUpdateCollectionAsync(existing);
        }

        public async Task RemoveFromCollectionAsync(int userId, int bookSeriesId)
        {
            await _collectionRepo.RemoveFromCollectionAsync(userId, bookSeriesId);
        }

        public async Task<List<CollectionItemDto>> SearchUserCollectionAsync(int userId, string keyword)
        {
            var collections = await _collectionRepo.SearchUserCollectionsAsync(userId, keyword);
            return collections.Select(c => new CollectionItemDto
            {
                BookSeriesId = c.BookSeriesId,
                SeriesName = c.BookSeries?.Name ?? "",
                CoverImageUrl = c.BookSeries?.CoverImageUrl ?? "",
                OwnedVolumes = c.OwnedVolumes,
                WishlistVolumes = c.WishlistVolumes
            }).ToList();
        }
        public async Task<List<UpcomingVolumeDto>> GetUpcomingUnownedVolumesAsync(int userId)
        {
            var volumes = await _collectionRepo.GetUpcomingUnownedVolumesAsync(userId);

            return volumes.Select(v => new UpcomingVolumeDto
            {
                SeriesId = v.BookSeriesId,
                SeriesName = v.BookSeries?.Name ?? "",
                VolumeNumber = v.VolumeNumber,
                ReleaseDate = v.ReleaseDate,
                CoverImageUrl = v.CoverImageUrl
            })
            .OrderBy(v => v.ReleaseDate)
            .ToList();
        }

    }
}
