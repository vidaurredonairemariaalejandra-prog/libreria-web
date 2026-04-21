using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibreriaAPI.Data;
using LibreriaAPI.Models;
using LibreriaAPI.DTOs.Usuario;

namespace LibreriaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly LibreriaContext _context;

        public UsuariosController(LibreriaContext context)
        {
            _context = context;
        }

        // ✅ GET: api/usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetUsuarios()
        {
            var usuarios = await _context.Usuarios.ToListAsync();

            var resultado = usuarios.Select(u => new UsuarioDTO
            {
                Id = u.Id,
                Nombre = u.Nombre,
                Email = u.Email
            });

            return Ok(resultado);
        }

        // ✅ POST: api/usuarios
        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> CrearUsuario(CrearUsuarioDTO dto)
        {
            var usuario = new Usuario
            {
                Nombre = dto.Nombre,
                Email = dto.Email
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            var resultado = new UsuarioDTO
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Email = usuario.Email
            };

            return Ok(resultado);
        }
    }
}