namespace Fragment.Domain.Repositories;

public interface ITagRepository
{
    Task<List<Tag>> GetAllAsync(CancellationToken ct);

    Task AddAsync(Tag tag, CancellationToken ct);

    Task DeleteAsync(int id, CancellationToken ct);
    
    Task<Tag?> GetByIdAsync(int id, CancellationToken cancellationToken);
}