using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace HWParts.Core.Infrastructure.Config.Maps
{
    //class MemoryMap : EntityBaseMap<Memory>, IEntityTypeConfiguration<Memory>, IEntityMap
    //{
    //    public void Configure(EntityTypeBuilder<Memory> builder)
    //    {
    //        DefaultMapping(builder, "TB_MEMORIES");

    //        builder.Property(e => e.Platform)
    //            .HasConversion(
    //                v => v.ToString(),
    //                v => (EPlatform)Enum.Parse(typeof(EPlatform), v));
    //    }
    //}
}
