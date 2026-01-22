#region

using System.Linq.Expressions;
using Common.Pagination;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Common.Data;

public interface IRepository<T> where T : EntityBase
{
    Task<T?> GetByIdAsync(Guid id, bool trackChanges = false, CancellationToken cancellationToken = default);

    Task<PagedList<T>> GetByConditionAsync(
        Expression<Func<T, bool>> expression, 
        PaginationParameters paginationParameters, 
        bool trackChanges = false,
        CancellationToken cancellationToken = default);
    
    Task<T?> UpdateAsync(Guid id, T updatedEntity, CancellationToken cancellationToken = default);
    
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}

public class Repository<T>(DbContext context) : IRepository<T> where T : EntityBase
{
    public Task<T?> GetByIdAsync(Guid id, bool trackChanges = false, CancellationToken cancellationToken = default)
    {
        return trackChanges
            ? context.Set<T>().FirstOrDefaultAsync(cancellationToken)
            : context.Set<T>().AsNoTracking().FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<PagedList<T>> GetByConditionAsync(
        Expression<Func<T, bool>> expression, 
        PaginationParameters paginationParameters,
        bool trackChanges = false,
        CancellationToken cancellationToken = default)
    {
        var query = context
            .Set<T>()
            .Where(expression);
        
        var count = await query.CountAsync(cancellationToken);
        
        query = query
            .Skip((paginationParameters.PageNumber - 1) * paginationParameters.PageSize)
            .Take(paginationParameters.PageSize);

        if (trackChanges)
        {
            query = query.AsNoTracking();
        }
        
        var list = await query.ToListAsync(cancellationToken);
        
        return list.ToPagedList(paginationParameters.PageNumber, paginationParameters.PageSize, count);
    }

    public async Task<T?> UpdateAsync(Guid id, T updatedEntity, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, true, cancellationToken);

        if (entity is null)
        {
            return null;
        }
        
        updatedEntity.Id = id;

        entity = updatedEntity;

        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, false, cancellationToken);

        if (entity is null)
        {
            return false;
        }
        
        context.Set<T>().Remove(entity);
        return true;
    }
} 
