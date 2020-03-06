using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWParts.Core.Infrastructure.Config.Maps
{
    class MemoryMap : EntityBaseMap<Memory>, IEntityTypeConfiguration<Memory>, IEntityMap
    {
        public void Configure(EntityTypeBuilder<Memory> builder)
        {
            DefaultMapping(builder, "TB_MEMORIES");

            builder.Property(x => x.Price)
                .HasColumnType("decimal(10, 4)");

            builder.Property(e => e.Platform)
                .HasConversion(
                    v => v.ToString(),
                    v => (EPlatform)Enum.Parse(typeof(EPlatform), v));
        }
    }
}
