using BLL.IServices;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.Controllers
{
    [ApiController]
    [Route("api/admin/books")]
    [Authorize(Roles = "Admin")]
    public class AdminBooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public AdminBooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // --- BookSeries ---
        [HttpPost("series")]
        public async Task<IActionResult> AddBookSeries([FromBody] BookSeries series)
        {
            await _bookService.AddBookSeriesAsync(series);
            return Ok(new { message = "Book series added." });
        }

        [HttpPut("series/{id}")]
        public async Task<IActionResult> UpdateBookSeries(int id, [FromBody] BookSeries series)
        {
            if (id != series.Id) return BadRequest();
            await _bookService.UpdateBookSeriesAsync(series);
            return Ok(new { message = "Book series updated." });
        }

        [HttpDelete("series/{id}")]
        public async Task<IActionResult> DeleteBookSeries(int id)
        {
            await _bookService.DeleteBookSeriesAsync(id);
            return Ok(new { message = "Book series deleted." });
        }

        // --- BookVolume ---
        [HttpPost("volume")]
        public async Task<IActionResult> AddBookVolume([FromBody] BookVolume volume)
        {
            await _bookService.AddBookVolumeAsync(volume);
            return Ok(new { message = "Book volume added." });
        }

        [HttpPut("volume/{id}")]
        public async Task<IActionResult> UpdateBookVolume(int id, [FromBody] BookVolume volume)
        {
            if (id != volume.Id) return BadRequest();
            await _bookService.UpdateBookVolumeAsync(volume);
            return Ok(new { message = "Book volume updated." });
        }

        [HttpDelete("volume/{id}")]
        public async Task<IActionResult> DeleteBookVolume(int id)
        {
            await _bookService.DeleteBookVolumeAsync(id);
            return Ok(new { message = "Book volume deleted." });
        }
    }
}
