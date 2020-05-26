using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace HWParts.Core.Infrastructure.Config.Maps
{
    class GraphicsCardMap : EntityBaseMap<GraphicsCard>, IEntityTypeConfiguration<GraphicsCard>, IEntityMap
    {
        public void Configure(EntityTypeBuilder<GraphicsCard> builder)
        {
            DefaultMapping(builder, "TB_GRAPHICS_CARDS");

            builder.Property(e => e.Platform)
                .HasConversion(
                    v => v.ToString(),
                    v => (EPlatform)Enum.Parse(typeof(EPlatform), v));
        }
    }
}
