using Application.Services.Interfaces;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClientesAPI.Controllers
{
    public class BaseController<TEntity, TDto, TService> : ControllerBase
        where TEntity : class, IBase
        where TDto : class, IBase
        where TService : IService<TEntity, TDto>
    {
        protected readonly TService _service;

        public BaseController(TService service)
        {
            _service = service;
        }
    }
}
