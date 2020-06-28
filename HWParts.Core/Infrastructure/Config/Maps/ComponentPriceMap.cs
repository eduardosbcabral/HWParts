using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Enums;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace HWParts.Core.Infrastructure.Config.Maps
{
    public class ComponentPriceMap : IEntityTypeConfiguration<ComponentPrice>
    {
        public void Configure(EntityTypeBuilder<ComponentPrice> builder)
        {
            builder.ToTable("TB_COMPONENTS_PRICES");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.UpdatedAt)
                .IsRequired(false);

            builder.Property(x => x.Price)
                .HasColumnType("decimal(19,4)");

            builder.Property(x => x.Url)
                .IsRequired(true);

            var converter = new EnumToStringConverter<Platform>();
            builder
                .Property(x => x.Platform)
                .HasConversion(
                    v => v.ToString(),
                    v => (EPlatform)Enum.Parse(typeof(EPlatform), v));

            builder.HasOne(x => x.Component)
                .WithMany(x => x.Prices);
        }
    }
}
