using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Almacen.Models.Data

{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }


    }
}
