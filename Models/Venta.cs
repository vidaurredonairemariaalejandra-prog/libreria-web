using System;
using System.Collections.Generic;

namespace LibreriaAPI.Models
{
    public class Venta
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }

        // Relación con Usuario
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        // Relación con DetalleVenta
        public List<DetalleVenta> Detalles { get; set; }
    }
}