namespace Domain.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync<TEntity>(int id) where TEntity : class, IBase;
        Task AddAsync(TEntity entity);
    }
}
