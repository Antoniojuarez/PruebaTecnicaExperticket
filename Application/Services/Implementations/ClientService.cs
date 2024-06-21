using Application.Dtos;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Services.Implementations
{
    public class ClientService : Service<Client, ClientDto>, IClientService
    {
        private readonly IRepositoryBase<Client> _clientRepository;
        private readonly IMapper _mapper;

        public ClientService(IRepositoryBase<Client> clientRepository, IMapper mapper) : base(clientRepository, mapper)
        {
            _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
        }
    }
}
