using BLL.DTOs;
using BLL.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookCollectionController : ControllerBase
    {
        private readonly IBookCollectionService _bookCollectionService;

        public BookCollectionController(IBookCollectionService bookCollectionService)
        {
            _bookCollectionService = bookCollectionService;
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> AddToCollection([FromBody] AddToCollectionDto dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized(new { message = "User ID not found in token." });

            int userId = int.Parse(userIdClaim.Value);

            await _bookCollectionService.AddToCollectionAsync(userId, dto);
            return Ok(new { message = "Collection updated successfully" });
        }

        [Authorize]
        [HttpGet("collection")]
        public async Task<IActionResult> GetUserCollection()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            var collection = await _bookCollectionService.GetUserCollectionAsync(userId);
            return Ok(collection);
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateCollection([FromBody] UpdateCollectionDto dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized(new { message = "User ID not found in token." });

            int userId = int.Parse(userIdClaim.Value);

            await _bookCollectionService.UpdateCollectionAsync(userId, dto);
            return Ok(new { message = "Collection updated successfully" });
        }

        [Authorize]
        [HttpDelete("remove/{bookSeriesId}")]
        public async Task<IActionResult> RemoveFromCollection(int bookSeriesId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized(new { message = "User ID not found in token." });

            int userId = int.Parse(userIdClaim.Value);

            await _bookCollectionService.RemoveFromCollectionAsync(userId, bookSeriesId);
            return Ok(new { message = "Series removed from collection." });
        }

        [Authorize]
        [HttpGet("search")]
        public async Task<IActionResult> SearchUserCollection([FromQuery] string keyword)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized(new { message = "User ID not found in token." });

            int userId = int.Parse(userIdClaim.Value);
            var result = await _bookCollectionService.SearchUserCollectionAsync(userId, keyword);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("upcoming-volumes")]
        public async Task<IActionResult> GetUpcomingVolumes()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized(new { message = "User ID not found in token." });

            int userId = int.Parse(userIdClaim.Value);

            var result = await _bookCollectionService.GetUpcomingUnownedVolumesAsync(userId);
            return Ok(result);
        }
    }
}
