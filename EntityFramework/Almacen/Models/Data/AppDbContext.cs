using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Biblioteca.Models.Entities;

namespace Almacen.Models.Data

{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<Biblioteca.Models.Entities.Libro> Libro { get; set; } = default!;
        public DbSet<Biblioteca.Models.Entities.Prestamo> Prestamo { get; set; } = default!;
        public DbSet<Biblioteca.Models.Entities.Usuario> Usuario { get; set; } = default!;


    }
}
