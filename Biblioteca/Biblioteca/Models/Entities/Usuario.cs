namespace Biblioteca.Models.Entities
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }

        public ICollection<Prestamo> Prestamo { get; set; } = new List<Prestamo>();

        public Usuario()
        {
        }

        public Usuario(Guid id, string nombre, string correo)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Correo = correo;
            Prestamo = new List<Prestamo>();
        }
    }
}
