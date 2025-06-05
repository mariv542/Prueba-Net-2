using System.Security.Cryptography.X509Certificates;

namespace Biblioteca.Models.Entities
{
    public class Prestamo
    {
        public Guid Id { get; set; }
        public Guid LibroId {  get; set; }
        public Libro Libro { get; set; }

        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime? FechaDevolucion { get; set; }

        public Prestamo()
        {
            
        }
        public Prestamo(Guid id, Guid libroId, Guid usuarioId, DateTime fechaPrestamo, DateTime? fechaDevolucion = null)
        {
            this.Id = id;
            this.LibroId = libroId;
            this.UsuarioId = usuarioId;
            this.FechaPrestamo = fechaPrestamo;
            this.FechaDevolucion = fechaDevolucion;
        }
    }
}
