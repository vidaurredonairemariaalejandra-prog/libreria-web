namespace LibreriaAPI.Models
{
    public class DetalleVenta
    {
        public int Id { get; set; }

        public int VentaId { get; set; }
        public Venta Venta { get; set; } = null!;

        public int LibroId { get; set; }
        public Libro Libro { get; set; } = null!;

        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
    }
}