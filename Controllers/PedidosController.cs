using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibreriaAPI.Data;
using LibreriaAPI.Models;

namespace LibreriaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly LibreriaContext _context;

        public PedidosController(LibreriaContext context)
        {
            _context = context;
        }

        // GET: api/pedidos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
        {
            return await _context.Pedidos
                .Include(p => p.Usuario)
                .Include(p => p.Detalles)
                .ToListAsync();
        }

        // POST: api/pedidos
        [HttpPost]
        public async Task<ActionResult> CrearPedido(Pedido pedido)
        {
            // 🔴 Validación básica
            if (pedido.Detalles == null || pedido.Detalles.Count == 0)
            return BadRequest("El pedido debe tener al menos un libro");

            // 🔎 Verificar usuario
           var usuario = await _context.Usuarios.FindAsync(pedido.UsuarioId);
          if (usuario == null)
          return BadRequest("El usuario no existe");

           pedido.Fecha = DateTime.Now;
           pedido.Estado = "Pendiente";

           // 🔁 Procesar cada detalle
          foreach (var detalle in pedido.Detalles)
            {
                 var libro = await _context.Libros.FindAsync(detalle.LibroId);

                  if (libro == null)
                   return BadRequest($"Libro con ID {detalle.LibroId} no existe");

                  if (libro.Stock < detalle.Cantidad)
                  return BadRequest($"Stock insuficiente para {libro.Titulo}");

                  // 🔥 Descontar stock
                  libro.Stock -= detalle.Cantidad;
            }
         _context.Pedidos.Add(pedido);
          await _context.SaveChangesAsync();

          return Ok(pedido);

        }
    }
}