using Application.Dtos;
using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IClientService : IService<Client, ClientDto>
    {
    }
}
