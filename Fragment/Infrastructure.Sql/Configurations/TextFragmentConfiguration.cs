using Fragment.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fragment.Infrastructure.Sql.Configurations;

public class TextFragmentConfiguration : IEntityTypeConfiguration<TextFragment>
{
    public void Configure(EntityTypeBuilder<TextFragment> builder)
    {
        builder.Property(f => f.Text).IsRequired().HasMaxLength(-1);
        builder.HasMany(f => f.Tags).WithMany().UsingEntity("TextFragmentTags");
    }
}
