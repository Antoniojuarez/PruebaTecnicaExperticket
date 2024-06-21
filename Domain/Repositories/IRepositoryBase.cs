namespace Domain.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>() where TEntity : class, IBase;
        Task<TEntity> GetByIdAsync<TEntity>(int id) where TEntity : class, IBase;
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(int id);
    }
}
