using Domain.Entities;
using Infraestructure;
using Microsoft.EntityFrameworkCore;

namespace ClientesAPITest
{
    public class DbInitializerTest
    {
        public static void Initialize(ClientContext context)
        {
            if (context.Clients.Any())
            {
                return;
            }

            Seed(context);
        }

        private static void Seed(ClientContext context)
        {
            var clients = new List<Client>
            {
                new Client { Name = "Antonio", Surname = "Juarez", Gender = Domain.Enums.Gender.Man, Country = Domain.Enums.Country.Spain, Email = "test@test.com"},
                new Client { Name = "Pierre", Surname = "Dubois", Gender = Domain.Enums.Gender.Man, Country = Domain.Enums.Country.France, Email = "testFrance@test.com"}
            };

            context.Clients.AddRange(clients);
            context.SaveChanges();
        }

    }
}
