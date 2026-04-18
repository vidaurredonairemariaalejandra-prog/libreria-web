using System;
using System.Collections.Generic;

namespace LibreriaAPI.Models
{
    public class Venta
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        public List<DetalleVenta> Detalles { get; set; } = new List<DetalleVenta>();
    }
}