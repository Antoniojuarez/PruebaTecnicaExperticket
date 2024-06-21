using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>, IDisposable
        where TEntity : class
    {
        private readonly ClientContext _context;

        public RepositoryBase(ClientContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>() where TEntity : class, IBase
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync<TEntity>(int id) where TEntity : class, IBase
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        // Falta mejora para no tener que pasar todos los parametros requeridos al actualizar
        public async Task UpdateAsync(TEntity objModel)
        {
            var entityType = _context.Model.FindEntityType(typeof(TEntity));
            if (entityType == null)
            {
                throw new InvalidOperationException($"Entity type {typeof(TEntity).Name} not found in the context");
            }

            var keyProperties = entityType.FindPrimaryKey().Properties;
            var keyValues = keyProperties.Select(p => p.PropertyInfo.GetValue(objModel)).ToArray();

            var existingEntity = await _context.Set<TEntity>().FindAsync(keyValues);
            if (existingEntity == null)
            {
                throw new InvalidOperationException("Entity not found in the context");
            }

            _context.Entry(existingEntity).CurrentValues.SetValues(objModel);

            _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);

            if (entity == null)
                return false;

            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool _disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            _disposed = true;
        }
    }
}
