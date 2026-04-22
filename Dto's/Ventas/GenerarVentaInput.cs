namespace LibreriaAPI.DTOs.Venta.GenerarVenta
{
    public class GenerarVentaInput
    {
        public int UsuarioId { get; set; }

        public string FormaPago { get; set; } = string.Empty;

        public List<ProductoVentaInput> Detalle { get; set; } = new();
    }

    public class ProductoVentaInput
    {
        public int LibroId { get; set; }
        public int Cantidad { get; set; }
    }
}