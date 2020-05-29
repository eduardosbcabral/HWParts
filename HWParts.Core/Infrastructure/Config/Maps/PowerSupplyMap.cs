using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace HWParts.Core.Infrastructure.Config.Maps
{
    class PowerSupplyMap : EntityBaseMap<PowerSupply>, IEntityTypeConfiguration<PowerSupply>, IEntityMap
    {
        public void Configure(EntityTypeBuilder<PowerSupply> builder)
        {
            DefaultMapping(builder, "TB_POWER_SUPPLIES");

            builder.Property(e => e.Platform)
                .HasConversion(
                    v => v.ToString(),
                    v => (EPlatform)Enum.Parse(typeof(EPlatform), v));
        }
    }
}
