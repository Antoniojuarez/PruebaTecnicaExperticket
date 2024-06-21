using Domain.Repositories;

namespace Application.Services.Interfaces
{
    public interface IService<TEntity, TDto>
        where TEntity : class, IBase
        where TDto : class
    {
        Task<TDto> GetByIdAsync(int id);
        Task<TDto> CreateAsync(TDto dto);
    }
}
