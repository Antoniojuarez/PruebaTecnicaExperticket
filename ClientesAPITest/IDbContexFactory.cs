using Infraestructure;

namespace ClientesAPITest
{
    public interface IDbContextFactory : IDisposable
    {
        ClientContext CreateDbContext();
    }
}
