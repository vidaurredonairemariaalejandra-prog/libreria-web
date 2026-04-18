namespace LibreriaAPI.Models
{
    public class DetallePedido
    {
        public int Id { get; set; }

        // Relación con Pedido
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; } = null!;

        // Relación con Libro
        public int LibroId { get; set; }
        public Libro Libro { get; set; } = null!;

        public int Cantidad { get; set; }
    }
}