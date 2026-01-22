

using Microsoft.EntityFrameworkCore;

namespace Common.Data;

public interface IRepositoryManager
{
    Task SaveAsync(CancellationToken cancellationToken = default);
}

public class RepositoryManager(DbContext context) : IRepositoryManager
{
    public Task SaveAsync(CancellationToken cancellationToken = default)
    {
        return context.SaveChangesAsync(cancellationToken);
    }
}
