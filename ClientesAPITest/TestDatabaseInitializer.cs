using Infraestructure;
using Microsoft.EntityFrameworkCore;

namespace ClientesAPITest
{
    public static class TestDatabaseInitializer
    {
        private static ClientContext _context;

        public static ClientContext Initialize()
        {
            if (_context == null)
            {
                var options = new DbContextOptionsBuilder<ClientContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                    .Options;

                _context = new ClientContext(options);
                DbInitializerTest.Initialize(_context); // Opcional: inicializar datos de prueba
            }

            return _context;
        }

        public static void Cleanup()
        {
            if (_context != null)
            {
                _context.Database.EnsureDeleted();
                _context.Dispose();
                _context = null;
            }
        }
    }

}
