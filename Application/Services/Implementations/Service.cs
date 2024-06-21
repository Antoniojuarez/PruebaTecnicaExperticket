using Application.Services.Interfaces;
using Application.Utils;
using AutoMapper;
using Domain.Repositories;

namespace Application.Services.Implementations
{
    public class Service<TEntity, TDto> : IService<TEntity, TDto>
        where TEntity : class, IBase
        where TDto : class
    {

        protected readonly IRepositoryBase<TEntity> _repository;
        protected readonly IMapper _mapper;

        public Service(IRepositoryBase<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TDto> CreateAsync(TDto dto)
        {
            ValidationUtils.AgainstNull(dto, nameof(dto));
            var entity = _mapper.Map<TEntity>(dto);
            await _repository.AddAsync(entity);
            return _mapper.Map<TDto>(entity);
        }
    }
}
