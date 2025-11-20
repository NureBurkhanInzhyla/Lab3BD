using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab3BD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController: ControllerBase
    {
        private readonly MusicContext _context;
        public GenresController(MusicContext context) => _context = context;

        [HttpGet("{id}")]
        public async Task<ActionResult> GetGenre(int id)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(g => g.GenreId == id);

            if (genre == null)
                return NotFound();

            return Ok(genre);
        }
    }
}
