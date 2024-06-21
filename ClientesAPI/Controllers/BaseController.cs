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

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TDto>>> GetAll()
        {
            var entities = await _service.GetAllAsync();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<OperationResult<TDto>>> GetById(int id)
        {
            var result = new OperationResult<TDto>();

            var entity = await _service.GetByIdAsync(id);
            
            if (entity == null)
            {
                result.AddMessage($"{typeof(TDto).Name} not found");
                return NotFound(result);
            }

            result.SetSuccessResponse(entity);
            return Ok(result);
        }

        [HttpPost]
        public virtual async Task<ActionResult<OperationResult<TDto>>> Create(TDto dto)
        {
            var result = new OperationResult<TDto>();
            var newEntity = await _service.CreateAsync(dto);
            result.SetSuccessResponse(newEntity);
            return CreatedAtAction("Create", new { id = newEntity.Id }, result);
        }

        [HttpPut("{id}")]
        public virtual async Task<ActionResult<OperationResult<TDto>>> Update(int id, TDto dto)
        {
            var result = new OperationResult<TDto>();

            if (id != dto.Id)
            {
                result.AddMessage($"Invalid {typeof(TDto).Name} ID.");
                return BadRequest(result);
            }

            var updateEntity = await _service.UpdateAsync(dto);

            if (updateEntity == null)
            {
                result.AddMessage($"{typeof(TDto).Name} not found.");
                return NotFound(result);
            }

            result.SetSuccessResponse(updateEntity);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult<OperationResult<bool>>> Delete(int id)
        {
            var result = new OperationResult<bool>();

            if (await _service.DeleteAsync(id))
            {
                result.SetSuccessResponse(true);
            }
            else
            {
                result.AddMessage($"{typeof(TDto).Name} not found.");
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}
