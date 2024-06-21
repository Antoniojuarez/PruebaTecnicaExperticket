using Application.Services.Implementations;
using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Repositories;
using Infraestructure;
using Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddDbContext<ClientContext>(options => 
    options.UseInMemoryDatabase(databaseName: "ClientsDb"));

builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
builder.Services.AddScoped(typeof(IService<,>), typeof(Service<,>));
builder.Services.AddScoped<IClientService, ClientService>();

builder.Services.AddControllers().
    AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ClientContext>();

    if (!context.Clients.Any())
    {
        var clients = new[]
        {
                        new Client { Name = "Antonio", Surname = "Juarez", Gender = Domain.Enums.Gender.Man, Country = Domain.Enums.Country.Spain, Email = "test@test.com"},
                        new Client { Name = "Pierre", Surname = "Dubois", Gender = Domain.Enums.Gender.Man, Country = Domain.Enums.Country.France, Email = "testFrance@test.com"}
                    };

        context.Clients.AddRange(clients);
        context.SaveChanges();
    }
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseCors("AllowAllOrigins");

app.MapGet("/", () => "Hello World!");

app.Run();
