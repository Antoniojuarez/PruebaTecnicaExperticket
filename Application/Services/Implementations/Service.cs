using Application.Services.Interfaces;
using Domain.Repositories;

namespace Application.Services.Implementations
{
    public class Service<TEntity, TDto> : IService<TEntity, TDto>
        where TEntity : class, IBase
        where TDto : class
    {
    }
}
