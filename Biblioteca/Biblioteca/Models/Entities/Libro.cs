namespace Biblioteca.Models.Entities
{
    public class Libro
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Editorial { get; set; }
        public int AñoPublicacion { get; set; }
        public bool EstadoPrestado { get; set; }

        public ICollection<Prestamo> Prestamos { get; set; }

        public Libro()
        {
        }

        public Libro(Guid id, string titulo, string autor, string editorial, int añoPublicacion, bool estadoPrestamo)
        {
            this.Id = id;
            this.Titulo = titulo;
            this.Autor = autor;
            this.Editorial = editorial;
            this.AñoPublicacion = añoPublicacion;
            this.EstadoPrestado = estadoPrestamo;

        }
    }
}

