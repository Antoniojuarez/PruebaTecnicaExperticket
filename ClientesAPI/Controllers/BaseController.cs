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

        [HttpPost]
        public virtual async Task<ActionResult<OperationResult<TDto>>> Create(TDto dto)
        {
            var result = new OperationResult<TDto>();
            var newEntity = await _service.CreateAsync(dto);
            result.SetSuccessResponse(newEntity);
            return CreatedAtAction("Create", new { id = newEntity.Id }, result);
        }
    }
}
