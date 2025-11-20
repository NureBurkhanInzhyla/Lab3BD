using Lab3BD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab3BD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumsController : ControllerBase
    {
        private readonly MusicContext _context;
        public AlbumsController(MusicContext context) => _context = context;

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAlbum(int id)
        {
            var album = await _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Label)
                .Include(a => a.Tracks)
                .FirstOrDefaultAsync(a => a.AlbumId == id);

            if (album == null)
                return NotFound();

            return Ok(album);
        }
    }
}
