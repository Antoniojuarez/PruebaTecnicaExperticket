using Domain.Entities;
using Infraestructure;
using Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClientesAPITest
{
    public class RepositoryBaseTests
    {
        private readonly DbContextOptions<ClientContext> _dbClientContextOptions;

        private readonly ClientContext _context;

        public RepositoryBaseTests() 
        {
            _dbClientContextOptions = new DbContextOptionsBuilder<ClientContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new ClientContext(_dbClientContextOptions);

            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            SeedDatabase().Wait();
        }

        private async Task SeedDatabase()
        {
            var clients = new List<Client>
            {
                new Client { ClientId = 1, Name = "Antonio", Surname = "Juarez", Gender = Domain.Enums.Gender.Man, Country = Domain.Enums.Country.Spain, Email = "test@test.com"}
            };

            _context.Clients.AddRange(clients);
            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task Add_Client_ShouldAddEntity()
        {
            var repository = new RepositoryBase<Client>(_context);

            var client = new Client { ClientId = 2, Name = "Eva", Surname = "Garcia", Gender = Domain.Enums.Gender.Woman, Country = Domain.Enums.Country.Spain, Email = "test2@test.com" };

            await repository.AddAsync(client);
            var newClient = await _context.Clients.FindAsync(2);

            Assert.NotNull(newClient);
            Assert.Equal(client.Name, newClient.Name);
            Assert.Equal(client.Surname, newClient.Surname);
        }
    }
}
