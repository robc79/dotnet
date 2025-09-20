namespace Fragment.Domain.Repositories;

public interface IUnitOfWork
{
    Task CommitChangesAsync(CancellationToken ct);
}