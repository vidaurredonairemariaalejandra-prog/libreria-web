namespace LibreriaAPI.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        // Relación: una categoría tiene muchos libros
        public List<Libro> Libros { get; set; }
    }
}