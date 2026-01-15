using Hyperativa.Api.Helper;
using Hyperativa.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hyperativa.Api.Data.Mappings
{
    public class CreditCardMapping : IEntityTypeConfiguration<CreditCard>
    {
        public void Configure(EntityTypeBuilder<CreditCard> builder)
        {
            builder.ToTable("CreditCards");

            builder.HasKey(c => c.Id);

            builder.Property(x => x.CardNumber)
                   .HasConversion(new EncryptionConverter());

            builder.Property(x => x.CardNumberHash)
                   .IsRequired()
                   .HasMaxLength(64);

            builder.Property(c => c.LotPosition)
                   .HasMaxLength(20);

            builder.Property(c => c.LotLineIdentifier)
                   .HasMaxLength(20);
                        
            builder.Property(c => c.LotId)
                   .IsRequired(false);
        }
    }
}
