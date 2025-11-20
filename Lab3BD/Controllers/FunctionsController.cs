using Microsoft.AspNetCore.Mvc;

namespace Lab3BD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FunctionsController: ControllerBase
    {
        private readonly MusicService _service;

        public FunctionsController(MusicService service)
        {
            _service = service;
        }

        [HttpGet("lessThanAvg")]
        public async Task<IActionResult> LessThanAvg()
        {
            var result = await _service.GetTracksBelowAverageAsync();
            return Ok(result); 
        }

        [HttpGet("longerThan/{duration}")]
        public async Task<IActionResult> LongerThan(int duration)
        {
            var result = await _service.GetTracksLongerThanAsync(duration);
            return Ok(result); 
        }

        [HttpGet("topAlbums/{artistName}")]
        public async Task<IActionResult> TopAlbums(string artistName)
        {
            var result = await _service.GetTopAlbumsByArtistAsync(artistName);
            return Ok(result);
        }
    }
}
