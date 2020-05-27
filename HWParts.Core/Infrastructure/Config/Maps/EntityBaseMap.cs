using HWParts.Core.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HWParts.Core.Infrastructure.Config.Maps
{
    internal class EntityBaseMap<TEntityBase> where TEntityBase : EntityBase
    {
        public void DefaultMapping(EntityTypeBuilder<TEntityBase> builder, string tableName)
        {
            builder.ToTable(tableName);
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.UpdatedAt)
                .IsRequired(false);
        }
    }
}
