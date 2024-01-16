using API_BasicStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API_BasicStore
{
    public class DBContext : DbContext
    {
        public DBContext( DbContextOptions options ) : base (options)
        {}

        public DbSet<User> Usuario { get; set; }
        public DbSet<Producto> Productos { get; set; }

        public DbSet<SessionToken> Token { get; set; }
    }

    
}
