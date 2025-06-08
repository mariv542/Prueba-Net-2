using BibliotecaMunicipal.Models.Entities;

namespace Biblioteca.Models.Entities
{
    public class Prestamo
    {
        public Guid Id { get; set; }
        public Guid LibroId { get; set; }
        public Libro Libro { get; set; }

        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public required DateTime FechaPrestamo { get; set; }
        public DateTime? FechaDevolucion { get; set; }

        public Prestamo()
        {

        }
        public Prestamo(Guid id, Guid libroId, Guid usuarioId, DateTime fechaPrestamo, DateTime? fechaDevolucion = null)
        {
            if (fechaPrestamo > DateTime.Now)
                throw new ArgumentException("La fecha de prestamo no puede estar representada en el futuro", nameof(fechaPrestamo));
            if (fechaDevolucion.HasValue && fechaDevolucion.Value < fechaPrestamo)
                throw new ArgumentException("La fecha de devolucion no pue ser anterior a la fecha de prestamo", nameof(fechaDevolucion));

            this.Id = id;
            this.LibroId = libroId;
            this.UsuarioId = usuarioId;
            this.FechaPrestamo = fechaPrestamo;
            this.FechaDevolucion = fechaDevolucion;
        }
    }
}
