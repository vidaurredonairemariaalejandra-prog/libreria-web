namespace LibreriaAPI.DTOs.Pedidos.ListarPedidos
{
    public class ListarPedidosOutput
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string UsuarioNombre { get; set; } = string.Empty;

        public List<DetallePedidoOutput> Detalles { get; set; } = new();
    }

    public class DetallePedidoOutput
    {
        public string LibroTitulo { get; set; } = string.Empty;
        public int Cantidad { get; set; }
    }
}