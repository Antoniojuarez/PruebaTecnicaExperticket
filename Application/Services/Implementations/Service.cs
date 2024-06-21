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

        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync<TEntity>();
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        public async Task<TDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync<TEntity>(id);
            return _mapper.Map<TDto>(entity);
        }

        public async Task<TDto> CreateAsync(TDto dto)
        {
            ValidationUtils.AgainstNull(dto, nameof(dto));
            var entity = _mapper.Map<TEntity>(dto);
            await _repository.AddAsync(entity);
            return _mapper.Map<TDto>(entity);
        }

        public async Task<TDto> UpdateAsync(TDto dto)
        {
            ValidationUtils.AgainstNull(dto, nameof(dto));
            var entity = _mapper.Map<TEntity>(dto);
            await _repository.UpdateAsync(entity);
            return _mapper.Map<TDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
