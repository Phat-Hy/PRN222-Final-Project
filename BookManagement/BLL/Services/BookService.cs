using BLL.DTOs;
using BLL.IServices;
using DAL.IRepositories;
using DAL.Models;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<BookDetailsDto?> GetBookDetailsAsync(int id)
    {
        var book = await _bookRepository.GetBookDetailsAsync(id);
        if (book == null) return null;

        return new BookDetailsDto
        {
            Name = book.Name,
            Author = book.Author,
            Illustrator = book.Illustrator,
            Translator = book.Translator,
            CoverImageUrl = book.CoverImageUrl,
            Description = book.Description,
            ReleaseDate = book.Volumes.OrderBy(v => v.ReleaseDate).FirstOrDefault()?.ReleaseDate,
            Volumes = book.Volumes
                .OrderBy(v => v.VolumeNumber)
                .Select(v => new VolumeInfoDto
                {
                    Id = v.Id,
                    VolumeNumber = v.VolumeNumber,
                    ReleaseDate = v.ReleaseDate
                }).ToList()
        };
    }

    public async Task<BookVolumeDetailsDto?> GetVolumeDetailsAsync(int volumeId)
    {
        var volume = await _bookRepository.GetVolumeDetailsAsync(volumeId);
        if (volume == null) return null;

        return new BookVolumeDetailsDto
        {
            Id = volume.Id,
            VolumeNumber = volume.VolumeNumber,
            ChapterFrom = volume.ChapterFrom,
            ChapterTo = volume.ChapterTo,
            ReleaseDate = volume.ReleaseDate,
            Price = volume.Price,
            CoverImageUrl = volume.CoverImageUrl,
            Description = volume.Description,
            BookSeriesId = volume.BookSeriesId,
            SeriesName = volume.BookSeries?.Name ?? ""
        };
    }

    public async Task<List<BookSeriesListDto>> GetAllBookSeriesAsync()
    {
        var series = await _bookRepository.GetAllBookSeriesAsync();
        return series.Select(s => new BookSeriesListDto
        {
            Id = s.Id,
            Name = s.Name,
            CoverImageUrl = s.CoverImageUrl,
            Author = s.Author,
            Description = s.Description
        }).ToList();
    }
    public async Task<List<BookSeriesListDto>> SearchBookSeriesByNameAsync(string keyword)
    {
        var seriesList = await _bookRepository.SearchBookSeriesByNameAsync(keyword);

        return seriesList.Select(s => new BookSeriesListDto
        {
            Id = s.Id,
            Name = s.Name,
            Author = s.Author,
            Description = s.Description,
            CoverImageUrl = s.CoverImageUrl
        }).ToList();
    }
    public async Task AddBookSeriesAsync(BookSeries series)
    {
        await _bookRepository.AddBookSeriesAsync(series);
    }

    public async Task UpdateBookSeriesAsync(BookSeries series)
    {
        await _bookRepository.UpdateBookSeriesAsync(series);
    }

    public async Task DeleteBookSeriesAsync(int id)
    {
        await _bookRepository.DeleteBookSeriesAsync(id);
    }

    public async Task AddBookVolumeAsync(BookVolume volume)
    {
        await _bookRepository.AddBookVolumeAsync(volume);
    }

    public async Task UpdateBookVolumeAsync(BookVolume volume)
    {
        await _bookRepository.UpdateBookVolumeAsync(volume);
    }

    public async Task DeleteBookVolumeAsync(int id)
    {
        await _bookRepository.DeleteBookVolumeAsync(id);
    }

}
