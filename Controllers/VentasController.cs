using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibreriaAPI.Data;
using LibreriaAPI.DTOs.Venta.GenerarVenta;
using LibreriaAPI.Models;

namespace LibreriaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentasController : ControllerBase
    {
        private readonly LibreriaContext _context;

        public VentasController(LibreriaContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> GenerarVenta(GenerarVentaInput entrada)
        {
            // 🔒 TRANSACCIÓN (CLAVE para level point)
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // 🔴 Validar usuario
                var usuario = await _context.Usuarios.FindAsync(entrada.UsuarioId);
                if (usuario == null)
                    return BadRequest("Usuario no existe");

                // 🔴 Validar detalle
                if (entrada.Detalle == null || entrada.Detalle.Count == 0)
                    return BadRequest("Debe tener al menos un libro");

                var venta = new Venta
                {
                    Fecha = DateTime.Now,
                    UsuarioId = entrada.UsuarioId,
                    Detalles = new List<DetalleVenta>()
                };

                foreach (var item in entrada.Detalle)
                {
                    // 🔥 IMPORTANTE: Traer el libro con tracking
                    var libro = await _context.Libros
                        .FirstOrDefaultAsync(l => l.Id == item.LibroId);

                    if (libro == null)
                        return BadRequest($"Libro con ID {item.LibroId} no existe");

                    if (libro.Stock < item.Cantidad)
                        return BadRequest($"Stock insuficiente para {libro.Titulo}");

                    // 🔥 Descontar stock
                    libro.Stock -= item.Cantidad;

                    venta.Detalles.Add(new DetalleVenta
                    {
                        LibroId = libro.Id,
                        Cantidad = item.Cantidad,
                        Precio = libro.Precio
                    });
                }

                _context.Ventas.Add(venta);

                await _context.SaveChangesAsync();

                // 🔒 CONFIRMAR TRANSACCIÓN
                await transaction.CommitAsync();

                return Ok(new
                {
                    mensaje = "Venta realizada correctamente",
                    ventaId = venta.Id
                });
            }
            catch (Exception)
            {
                // 🔒 SI ALGO FALLA → rollback
                await transaction.RollbackAsync();
                return StatusCode(500, "Error al procesar la venta");
            }
        }
    }
}