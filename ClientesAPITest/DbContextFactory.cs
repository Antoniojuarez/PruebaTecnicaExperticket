using Infraestructure;
using Microsoft.EntityFrameworkCore;

namespace ClientesAPITest
{
    public class DbContextFactory : IDbContextFactory
    {
        private readonly DbContextOptions<ClientContext> _options;
        private readonly List<ClientContext> _contexts = new List<ClientContext>();

        public DbContextFactory(DbContextOptions<ClientContext> options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public ClientContext CreateDbContext()
        {
            var context = new ClientContext(_options);
            _contexts.Add(context);

            return context;
        }

        public void Dispose()
        {
            foreach (var context in _contexts)
            {
                context.Database.EnsureDeleted();
                context.Dispose();
            }
        }
    }
}
