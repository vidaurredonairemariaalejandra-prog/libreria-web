namespace LibreriaAPI.Models
{
    public class Pedido
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public string Estado { get; set; } = string.Empty; // ✔ evitar null

        // Relación con Usuario
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!; // ✔ EF se encarga

        // Relación con Detalles
        public List<DetallePedido> Detalles { get; set; } = new List<DetallePedido>(); // ✔ lista inicializada
    }
}