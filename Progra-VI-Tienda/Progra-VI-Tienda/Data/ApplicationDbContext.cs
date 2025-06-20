using Progra_VI_Tienda.Models;
using Microsoft.EntityFrameworkCore;

namespace Progra_VI_Tienda.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Categoria> Categoria { get; set; }

    }

}
