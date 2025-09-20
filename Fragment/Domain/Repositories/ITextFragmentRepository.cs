namespace Fragment.Domain.Repositories;

public interface ITextFragmentRepository
{
    Task<TextFragment?> GetByIdAsync(int id, CancellationToken ct);

    Task<List<TextFragment>> GetPageAsync(int skip, int take, CancellationToken ct);

    Task AddAsync(TextFragment fragment, CancellationToken ct);

    Task DeleteAsync(int id, CancellationToken ct);
}