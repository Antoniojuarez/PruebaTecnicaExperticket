using Infraestructure;
using Microsoft.EntityFrameworkCore;

namespace ClientesAPITest
{
    public class DatabaseFixture : IDisposable
    {
        public ClientContext Context { get; private set; }

        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<ClientContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            Context = new ClientContext(options);
            DbInitializerTest.Initialize(Context); // Opcional: inicializar datos de prueba
        }

        public void Dispose()
        {
            CleanupDatabase();
        }

        private void CleanupDatabase()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}
