namespace LibreriaAPI.DTOs.Pedidos.CrearPedido
{
    public class CrearPedidoOutput
    {
        public int Id { get; set; }
        public string Estado { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
    }
}