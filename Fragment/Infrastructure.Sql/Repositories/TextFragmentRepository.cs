using Fragment.Domain;
using Fragment.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Fragment.Infrastructure.Sql.Repositories;

public class TextFragmentRepository : ITextFragmentRepository
{
    private readonly FragmentDbContext _dbContext;

    public TextFragmentRepository(FragmentDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task AddAsync(TextFragment fragment, CancellationToken ct)
    {
        if (fragment is null)
        {
            throw new ArgumentNullException(nameof(fragment));
        }

        await _dbContext.TextFragments.AddAsync(fragment, ct);
    }

    public async Task DeleteAsync(int id, CancellationToken ct)
    {
        var fragment = await _dbContext.TextFragments.SingleOrDefaultAsync(f => f.Id == id, ct);
        
        if (fragment is not null)
        {
            _dbContext.TextFragments.Remove(fragment);
        }
    }

    public async Task<TextFragment?> GetByIdAsync(int id, CancellationToken ct)
    {
        return await _dbContext.TextFragments.Include(f => f.Tags).SingleOrDefaultAsync(f => f.Id == id, ct);
    }

    public async Task<List<TextFragment>> GetPageAsync(int skip, int take, CancellationToken ct)
    {
        return await _dbContext.TextFragments.Include(f => f.Tags).OrderBy(f => f.Id).Skip(skip).Take(take).ToListAsync(ct);
    }
}
