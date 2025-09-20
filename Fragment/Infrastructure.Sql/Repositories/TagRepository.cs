using Fragment.Domain;
using Fragment.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Fragment.Infrastructure.Sql.Repositories;

public class TagRepository : ITagRepository
{
    private readonly FragmentDbContext _dbContext;

    public TagRepository(FragmentDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));    
    }

    public async Task AddAsync(Tag tag, CancellationToken ct)
    {
        if (tag is null)
        {
            throw new ArgumentNullException(nameof(tag));
        }

        await _dbContext.Tags.AddAsync(tag, ct);
    }

    public async Task DeleteAsync(int id, CancellationToken ct)
    {
        var tag = await _dbContext.Tags.SingleOrDefaultAsync(t => t.Id == id, ct);

        if (tag is not null)
        {
            _dbContext.Tags.Remove(tag);
        }
    }

    public async Task<List<Tag>> GetAllAsync(CancellationToken ct)
    {
        return await _dbContext.Tags.ToListAsync(ct);
    }

    public async Task<Tag?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.Tags.SingleOrDefaultAsync(t => t.Id == id);
    }
}