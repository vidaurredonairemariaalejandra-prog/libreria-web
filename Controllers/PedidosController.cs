 using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibreriaAPI.Data;
using LibreriaAPI.Models;
using LibreriaAPI.DTOs.Pedidos.CrearPedido;
using LibreriaAPI.DTOs.Pedidos.ListarPedidos;

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

        // GET
        [HttpGet]
        public async Task<ActionResult<ICollection<ListarPedidosOutput>>> GetPedidos()
        {
            var pedidos = await _context.Pedidos
                .Include(p => p.Usuario)
                .Include(p => p.Detalles)
                    .ThenInclude(d => d.Libro)
                .ToListAsync();

            var resultado = pedidos.Select(p => new ListarPedidosOutput
            {
                Id = p.Id,
                Fecha = p.Fecha,
                Estado = p.Estado,
                UsuarioNombre = p.Usuario.Nombre,
                Detalles = p.Detalles.Select(d => new DetallePedidoOutput
                {
                    LibroTitulo = d.Libro.Titulo,
                    Cantidad = d.Cantidad
                }).ToList()
            }).ToList();

            return Ok(resultado);
        }

        // POST
        [HttpPost]
        public async Task<ActionResult<CrearPedidoOutput>> CrearPedido([FromBody] CrearPedidoInput dto)
        {
            if (dto.Detalles == null || dto.Detalles.Count == 0)
                return BadRequest("El pedido debe tener al menos un libro");

            var usuario = await _context.Usuarios.FindAsync(dto.UsuarioId);
            if (usuario == null)
                return BadRequest("El usuario no existe");

            var pedido = new Pedido
            {
                Fecha = DateTime.Now,
                Estado = "Pendiente",
                UsuarioId = dto.UsuarioId,
                Detalles = new List<DetallePedido>()
            };

            foreach (var detalle in dto.Detalles)
            {
                var libro = await _context.Libros.FindAsync(detalle.LibroId);

                if (libro == null)
                    return BadRequest($"Libro con ID {detalle.LibroId} no existe");

                if (libro.Stock < detalle.Cantidad)
                    return BadRequest($"Stock insuficiente para {libro.Titulo}");

                libro.Stock -= detalle.Cantidad;

                pedido.Detalles.Add(new DetallePedido
                {
                    LibroId = detalle.LibroId,
                    Cantidad = detalle.Cantidad
                });
            }

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            var salida = new CrearPedidoOutput
            {
                Id = pedido.Id,
                Estado = pedido.Estado,
                Fecha = pedido.Fecha
            };

            return CreatedAtAction(nameof(GetPedidos), new { id = salida.Id }, salida);
        }
    }
}        

    