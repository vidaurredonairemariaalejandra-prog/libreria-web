namespace LibreriaAPI.Models
{
    public class DetalleVenta
    {
        public int Id { get; set; }

        // Relación con Venta
        public int VentaId { get; set; }
        public Venta Venta { get; set; }

        // Relación con Libro
        public int LibroId { get; set; }
        public Libro Libro { get; set; }

        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
    }
}