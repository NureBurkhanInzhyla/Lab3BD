using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab3BD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArtistsController: ControllerBase
    {
        private readonly MusicContext _context;
        public ArtistsController(MusicContext context) => _context = context;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetArtist(int id)
        {
            var artist = await _context.Artists
                .Include(a => a.Albums)           
                .ThenInclude(al => al.Label)      
                .Include(a => a.Albums)
                .ThenInclude(al => al.Tracks)
                .FirstOrDefaultAsync(a => a.ArtistId == id);

            if (artist == null)
                return NotFound(new { status = "Error", message = "Artist not found" });

            return Ok(artist); 
        }
    }
}
