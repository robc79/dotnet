using Microsoft.EntityFrameworkCore;
using Fragment.Domain.Repositories;
using Fragment.Domain;

namespace Fragment.Infrastructure.Sql;

public class FragmentDbContext : DbContext, IUnitOfWork
{
    public FragmentDbContext(DbContextOptions<FragmentDbContext> options) : base(options)
    {
    }

    public DbSet<Tag> Tags { get; set; }

    public DbSet<TextFragment> TextFragments { get; set; }

    public async Task CommitChangesAsync(CancellationToken ct)
    {
        _ = await SaveChangesAsync(ct);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
