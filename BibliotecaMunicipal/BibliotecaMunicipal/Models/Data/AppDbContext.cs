using Microsoft.EntityFrameworkCore;
using BibliotecaMunicipal.Models.Entities;
using Biblioteca.Models.Entities;

namespace BibliotecaMunicipal.Models.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<BibliotecaMunicipal.Models.Entities.Libro> Libro { get; set; } = default!;
        public DbSet<Biblioteca.Models.Entities.Prestamo> Prestamo { get; set; } = default!;
        public DbSet<Biblioteca.Models.Entities.Usuario> Usuario { get; set; } = default!;
    }
}
