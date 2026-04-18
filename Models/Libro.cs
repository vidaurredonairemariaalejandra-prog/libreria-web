namespace LibreriaAPI.Models
{
    public class Libro
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public string Autor { get; set; } = null!;
        public decimal Precio { get; set; }
        public int Stock { get; set; }

        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; } = null!;
    }
}