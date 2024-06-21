using Application.Dtos;
using Application.Services.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ClientesAPI.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ClientController : BaseController<Client, ClientDto, IClientService>
    {
        public ClientController(IClientService service) : base(service) { }
    }
}
