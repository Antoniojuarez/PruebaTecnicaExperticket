using Domain.Entities;
using Infraestructure;
using Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClientesAPITest
{
    public class RepositoryBaseTests : IDisposable
    {
        private readonly DbContextOptions<ClientContext> _dbClientContextOptions;

        private readonly ClientContext _context;

        public RepositoryBaseTests() 
        {
            var options = new DbContextOptionsBuilder<ClientContext>()
                .UseInMemoryDatabase(databaseName: $"TestDb")
                .Options;

            this._context = new ClientContext(options);
            this._context.Database.EnsureCreated();

            DbInitializerTest.Initialize(this._context);

        }

        [Fact]
        public async Task GetById_Client_ShouldReturn()
        {
            var repository = new RepositoryBase<Client>(_context);

            var result = await repository.GetByIdAsync<Client>(2);

            Assert.NotNull(result);
            Assert.Equal(2, result.ClientId);
            Assert.Equal("Pierre", result.Name);
        }

        [Fact]
        public async Task GetById_Client_ShouldntReturn()
        {
            var repository = new RepositoryBase<Client>(_context);

            var result = await repository.GetByIdAsync<Client>(5);

            Assert.Null(result);
        }

        [Fact]
        public async Task Add_Client_ShouldAddEntity()
        {
            var repository = new RepositoryBase<Client>(_context);

            var client = new Client { Name = "Eva", Surname = "Garcia", Gender = Domain.Enums.Gender.Woman, Country = Domain.Enums.Country.Spain, Email = "test2@test.com" };

            await repository.AddAsync(client);
            var newClient = await _context.Clients.FindAsync(1);

            Assert.NotNull(newClient);
            Assert.Equal(client.Name, newClient.Name);
            Assert.Equal(client.Surname, newClient.Surname);
        }

        public void Dispose()
        {
            this._context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
