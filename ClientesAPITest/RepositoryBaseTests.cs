using Domain.Entities;
using Infraestructure;
using Infraestructure.Repositories;

namespace ClientesAPITest
{
    public class RepositoryBaseTests : IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture _fixture;
        private readonly ClientContext _context;

        public RepositoryBaseTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _context = _fixture.Context;
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
            var newClient = await _context.Clients.FindAsync(3);

            Assert.NotNull(newClient);
            Assert.Equal(client.Name, newClient.Name);
            Assert.Equal(client.Surname, newClient.Surname);
        }

        [Fact]
        public async Task Updat_Client_ShouldUpdateEntity()
        {
            var repository = new RepositoryBase<Client>(_context);

            var result = await repository.GetByIdAsync<Client>(2);

            result.Address = "Direccion Prueba";

            await repository.UpdateAsync(result);

            var updatedResult = await repository.GetByIdAsync<Client>(2);

            Assert.NotNull(updatedResult);
            Assert.Equal("Direccion Prueba", updatedResult.Address);
        }

        [Fact]
        public async Task Delete_Client_ShouldDeleteEntity()
        {
            var repository = new RepositoryBase<Client>(_context);

            var isDeleted = await repository.DeleteAsync(2);

            Assert.True(isDeleted);
        }
    }
}
