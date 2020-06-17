using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace HWParts.Core.Infrastructure.Config.Maps
{
    //class CaseMap : IEntityTypeConfiguration<Case>, IEntityMap
    //{
    //    public void Configure(EntityTypeBuilder<Case> builder)
    //    {
    //        //DefaultMapping(builder, "TB_CASES");

    //        builder.ToTable("TB_CASES");
    //        builder.HasKey(x => x.Id);

    //        builder.Property(e => e.Platform)
    //            .HasConversion(
    //                v => v.ToString(),
    //                v => (EPlatform)Enum.Parse(typeof(EPlatform), v));
    //    }
    //}
}
