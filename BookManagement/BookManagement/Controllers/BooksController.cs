using BLL.IServices;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookDetails(int id)
    {
        var book = await _bookService.GetBookDetailsAsync(id);
        if (book == null)
            return NotFound(new { message = "Book not found" });

        return Ok(book);
    }

    [HttpGet("volumes/{volumeId}")]
    public async Task<IActionResult> GetVolumeDetails(int volumeId)
    {
        var volume = await _bookService.GetVolumeDetailsAsync(volumeId);
        if (volume == null)
            return NotFound(new { message = "Volume not found" });

        return Ok(volume);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBookSeries()
    {
        var seriesList = await _bookService.GetAllBookSeriesAsync();
        return Ok(seriesList);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchBookSeries([FromQuery] string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
            return BadRequest(new { message = "Keyword is required" });

        var results = await _bookService.SearchBookSeriesByNameAsync(keyword);
        return Ok(results);
    }


}
