namespace LibreriaAPI.DTOs.Pedidos.CrearPedido
{
    public class CrearPedidoInput
    {
        public int UsuarioId { get; set; }
        public List<DetallePedidoInput> Detalles { get; set; } = new();
    }

    public class DetallePedidoInput
    {
        public int LibroId { get; set; }
        public int Cantidad { get; set; }
    }
}