using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWParts.Core.Infrastructure.Config.Maps
{
    public class ComponentBaseMap : IEntityTypeConfiguration<ComponentBase>
    {
        public void Configure(EntityTypeBuilder<ComponentBase> builder)
        {
            builder.ToTable("TB_COMPONENTS");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.UpdatedAt)
                .IsRequired(false);

            builder
                .Property(e => e.Platform)
                .HasConversion(
                    v => v.ToString(),
                    v => (EPlatform)Enum.Parse(typeof(EPlatform), v));
        }
    }
}
