using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure
{
    public class ClientContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }

        public ClientContext(DbContextOptions<ClientContext> options) : base(options) 
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("ClientsDb");
        }
    }
}
