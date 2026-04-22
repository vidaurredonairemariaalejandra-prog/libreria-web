namespace LibreriaAPI.DTOs.Venta.GenerarVenta
{
    public class GenerarVentaOutput
    {
        public int VentaId { get; set; }
        public DateTime Fecha { get; set; }
        public string FormaPago { get; set; } = string.Empty;

        public decimal Total { get; set; }

        public List<DetalleVentaOutput> Detalles { get; set; } = new();
    }

    public class DetalleVentaOutput
    {
        public string Libro { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }
}