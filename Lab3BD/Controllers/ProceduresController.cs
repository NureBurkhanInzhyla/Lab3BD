using Lab3BD.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

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
                var message = await _service.ChangeAlbumLabelAsync(
                  dto.AlbumId,
                  dto.AlbumTitle,
                  dto.ArtistId,
                  dto.NewLabelId
              );

                return Ok(new { message });
            }
            catch (SqlException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("updateAlbumTitle")]
        public async Task<IActionResult> UpdateAlbumTitle([FromBody] UpdateAlbumTitleDto dto)
        {
            try
            {
                var message = await _service.UpdateAlbumTitleAsync(dto.AlbumId, dto.Suffix);

                return Ok(new { message });
            }
            catch (SqlException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        
        }
    }
}
