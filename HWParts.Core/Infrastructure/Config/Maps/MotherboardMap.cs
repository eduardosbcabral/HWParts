using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace HWParts.Core.Infrastructure.Config.Maps
{
    class MotherboardMap : EntityBaseMap<Motherboard>, IEntityTypeConfiguration<Motherboard>, IEntityMap
    {
        public void Configure(EntityTypeBuilder<Motherboard> builder)
        {
            DefaultMapping(builder, "TB_MOTHERBOARDS");

            builder.Property(e => e.Platform)
                .HasConversion(
                    v => v.ToString(),
                    v => (EPlatform)Enum.Parse(typeof(EPlatform), v));
        }
    }
}
