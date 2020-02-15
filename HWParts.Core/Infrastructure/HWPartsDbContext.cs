using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;

namespace HWParts.Core.Infrastructure
{
    public class HWPartsDbContext : DbContext
    {
        public HWPartsDbContext(DbContextOptions<HWPartsDbContext> options) : base(options)
        {

        }

        public DbSet<Processor> Processors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Processor>()
                .Property(e => e.Platform)
                .HasConversion(
                    v => v.ToString(),
                    v => (EPlatform)Enum.Parse(typeof(EPlatform), v));
        }
    }
}
