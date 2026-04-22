namespace LibreriaAPI.DTOs
{
    public class PedidoCreateDTO
    {
        public int UsuarioId { get; set; }

        public List<DetallePedidoCreateDTO> Detalles { get; set; } = new();
    }

    public class DetallePedidoCreateDTO
    {
        public int LibroId { get; set; }
        public int Cantidad { get; set; }
    }
}