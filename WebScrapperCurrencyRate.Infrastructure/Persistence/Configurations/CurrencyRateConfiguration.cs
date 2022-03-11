
using WebScrapperCurrencyRate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CleanArchitecture.Infrastructure.Persistence.Configurations
{
    public class CurrencyRateConfiguration : IEntityTypeConfiguration<CurrencyRate>
    {
        public void Configure(EntityTypeBuilder<CurrencyRate> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
               .ValueGeneratedOnAdd()
               .HasDefaultValue(Guid.NewGuid().ToString());
               //.HasDefaultValueSql("newsequentialid()"); 
             
            builder.Property(c => c.Currency)
                .HasMaxLength(5)
                .IsRequired();
               
            builder.Property(c => c.Date)  
               .IsRequired()
               .HasColumnType("Date");

            builder.Property(c => c.Time)
              .IsRequired()
              .HasColumnType("Time"); 
        }
    }
}


