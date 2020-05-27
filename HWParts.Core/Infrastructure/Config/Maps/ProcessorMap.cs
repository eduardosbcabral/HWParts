using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace HWParts.Core.Infrastructure.Config.Maps
{
    class ProcessorMap : EntityBaseMap<Processor>, IEntityTypeConfiguration<Processor>, IEntityMap
    {
        public void Configure(EntityTypeBuilder<Processor> builder)
        {
            DefaultMapping(builder, "TB_PROCESSORS");

            builder.Property(e => e.Platform)
                .HasConversion(
                    v => v.ToString(),
                    v => (EPlatform)Enum.Parse(typeof(EPlatform), v));
        }
    }
}
