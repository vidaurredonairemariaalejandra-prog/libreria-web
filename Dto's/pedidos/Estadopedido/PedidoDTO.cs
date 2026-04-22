namespace LibreriaAPI.DTOs
{
    public class PedidoDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }

        public string Estado { get; set; } = string.Empty;

        public string UsuarioNombre { get; set; } = string.Empty;

        public List<DetallePedidoDTO> Detalles { get; set; } = new();
    }

    public class DetallePedidoDTO
    {
        public string LibroTitulo { get; set; } = string.Empty;
        public int Cantidad { get; set; }
    }
}