using KanBox.Domain.Primitives;
using KanBox.Domain.Priorities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KanBox.Persistence;

public class PriorityEntityTypeConfiguration : IEntityTypeConfiguration<Priority>
{
    public void Configure(EntityTypeBuilder<Priority> builder)
    {
        builder.HasKey(priority => priority.Id);

        builder.Property(priority => priority.Id)
            .HasConversion
            (
                id => id.Value,
                value => new PriorityId(value)
            );

        builder.Property(priority => priority.Label)
            .HasMaxLength(Priority.LabelMaxLength)
            .IsRequired();

        builder.Property(priority => priority.Color)
            .HasConversion
            (
                color => color.Hex,
                value => new Color(value)
            )
            .HasMaxLength(Color.MaxHexLength)
            .IsRequired();
    }
}