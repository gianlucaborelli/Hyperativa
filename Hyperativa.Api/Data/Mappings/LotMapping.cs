using Hyperativa.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hyperativa.Api.Data.Mappings
{
    public class LotMapping : IEntityTypeConfiguration<Lot>
    {
        public void Configure(EntityTypeBuilder<Lot> builder)
        {
            builder.ToTable("Lots");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Id)
                .ValueGeneratedNever();

            builder.Property(l => l.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            builder.Property(l => l.Name)
                .IsRequired();

            builder.Property(l => l.LotIssueDate)
                .IsRequired();

            builder.Property(l => l.LotCode)
                .IsRequired();

            builder.Property(l => l.NumberOfRecords)
                .IsRequired();

            builder.HasMany(l => l.CreditCards)
                .WithOne(cc => cc.Lot)
                .HasForeignKey(cc => cc.LotId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(c => c.Name);
        }
    }
}
