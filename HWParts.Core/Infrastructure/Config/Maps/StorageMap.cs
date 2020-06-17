using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace HWParts.Core.Infrastructure.Config.Maps
{
    //class StorageMap : EntityBaseMap<Storage>, IEntityTypeConfiguration<Storage>, IEntityMap
    //{
    //    public void Configure(EntityTypeBuilder<Storage> builder)
    //    {
    //        DefaultMapping(builder, "TB_STORAGE");

    //        builder.Property(e => e.Platform)
    //            .HasConversion(
    //                v => v.ToString(),
    //                v => (EPlatform)Enum.Parse(typeof(EPlatform), v));
    //    }
    //}
}
