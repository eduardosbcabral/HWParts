using HWParts.Core.Domain.Entities;
using HWParts.Core.Infrastructure.Config;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace HWParts.Core.Infrastructure
{
    public class HWPartsDbContext : DbContext
    {
        public DbSet<Processor> Processors { get; set; }
        public DbSet<Motherboard> Motherboards { get; set; }
        public DbSet<GraphicsCard> GraphicsCards { get; set; }

        public HWPartsDbContext(DbContextOptions<HWPartsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var typesToRegister = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof(IEntityMap).IsAssignableFrom(x) && !x.IsAbstract)
                .ToList();

            foreach (var type in typesToRegister)
            {
                dynamic instance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(instance);
            }
        }
    }
}
