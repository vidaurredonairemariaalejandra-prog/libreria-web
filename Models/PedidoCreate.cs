namespace LibreriaAPI.Models
{
    public class PedidoCreate
    {
        public int UsuarioId { get; set; }

        public List<DetallePedidoCreate> Detalles { get; set; } = new List<DetallePedidoCreate>();
    }

    public class DetallePedidoCreate
    {
        public int LibroId { get; set; }
        public int Cantidad { get; set; }
    }
}