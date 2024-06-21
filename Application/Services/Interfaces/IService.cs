﻿using Domain.Repositories;

namespace Application.Services.Interfaces
{
    public interface IService<TEntity, TDto>
        where TEntity : class, IBase
        where TDto : class
    {
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto> GetByIdAsync(int id);
        Task<TDto> CreateAsync(TDto dto);
        Task<TDto> UpdateAsync(TDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
