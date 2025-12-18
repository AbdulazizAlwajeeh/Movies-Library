using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMVCApp.Data;
using MyMVCApp.Models;

namespace MyMVCApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MyDbContext _context;

        public MoviesController(MyDbContext context)
        {
            _context = context;
        }
        private bool MovieExists(int id)
        {
            return _context.Movies.Any(x => x.Id == id);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return await _context.Movies.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var obj = await _context.Movies.FindAsync(id);

            if (obj == null)
                return NotFound();

            return obj;
        }
        [HttpPost]
        public async Task<ActionResult<Movie>> CreateMovie(Movie obj)
        {
            _context.Movies.Add(obj);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMovie), new { id = obj.Id }, obj);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, Movie obj)
        {
            if (id != obj.Id)
                return BadRequest();

            _context.Entry(obj).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var obj = await _context.Movies.FindAsync(id);
            if (obj == null)
                return NotFound();

            _context.Movies.Remove(obj);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
