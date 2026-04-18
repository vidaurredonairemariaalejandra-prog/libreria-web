using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibreriaAPI.Data;
using LibreriaAPI.Models;

namespace LibreriaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibrosController : ControllerBase
    {
        private readonly LibreriaContext _context;

        public LibrosController(LibreriaContext context)
        {
            _context = context;
        }

        // GET: api/libros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Libro>>> GetLibros()
        {
            return await _context.Libros.Include(l => l.Categoria).ToListAsync();
        }

        // GET: api/libros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Libro>> GetLibro(int id)
        {
            var libro = await _context.Libros
                .Include(l => l.Categoria)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (libro == null)
                return NotFound();

            return libro;
        }

        // POST: api/libros
        [HttpPost]
        public async Task<ActionResult<Libro>> CrearLibro(Libro libro)
        {
            _context.Libros.Add(libro);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLibro), new { id = libro.Id }, libro);
        }

        // PUT: api/libros/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarLibro(int id, Libro libro)
        {
            if (id != libro.Id)
                return BadRequest();

            _context.Entry(libro).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/libros/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarLibro(int id)
        {
            var libro = await _context.Libros.FindAsync(id);

            if (libro == null)
                return NotFound();

            _context.Libros.Remove(libro);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}