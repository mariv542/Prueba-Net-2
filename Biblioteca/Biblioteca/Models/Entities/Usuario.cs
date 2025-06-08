using System.Text.RegularExpressions;

namespace Biblioteca.Models.Entities
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public required string Nombre { get; set; }
        public required string Correo { get; set; }

        public ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();

        public Usuario()
        {
        }

        public Usuario(Guid id, string nombre, string correo)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede estar vacio", nameof(nombre));
            
            if (!Regex.IsMatch(correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ArgumentException("El correo no tiene un formato valido", nameof(correo));

            this.Id = id;
            this.Nombre = nombre;
            this.Correo = correo;
            Prestamos = new List<Prestamo>();
        }
    }
}
