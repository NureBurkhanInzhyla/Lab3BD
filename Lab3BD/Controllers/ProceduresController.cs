using Lab3BD.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Lab3BD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProceduresController:ControllerBase
    {
        private readonly MusicService _service;
        public ProceduresController(MusicService service) => _service = service;

        [HttpPost("addLabel")]
        public async Task<IActionResult> AddLabel([FromBody] LabelDto dto)
        {
            await _service.AddNewLabelAsync(dto.LabelId, dto.Name, dto.Country, dto.FoundationYear);
            return Ok("Label added");
        }

        [HttpPost("changeAlbumLabel")]
        public async Task<IActionResult> ChangeAlbumLabel([FromBody] ChangeAlbumLabelDto dto)
        {
            try
            {
                var message = await _service.ChangeAlbumLabelAsync(dto.AlbumId, dto.AlbumTitle, dto.ArtistId, dto.NewLabelId);

                return Ok(new { status = "Success", message = message });
            }
            catch (CustomDatabaseException ex)
            {
                return BadRequest(new { status = "Database Error", message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = "Error", message = $"Internal error: {ex.Message}" });
            }
        }

        [HttpPost("updateAlbumTitle")]
        public async Task<IActionResult> UpdateAlbumTitle([FromBody] UpdateAlbumTitleDto dto)
        {
            try
            {
                
                var newTitle = await _service.UpdateAlbumTitleAsync(dto.AlbumId, dto.Suffix);

                return Ok(new { status = "Updated", newTitle = newTitle });
            }
            catch (CustomDatabaseException ex)
            {
                return BadRequest(new
                {
                    status = "Database Error",
                    message = ex.Message
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new { status = "Error", message = "An internal server error occurred during title update." });
            }
        }
    }
}
