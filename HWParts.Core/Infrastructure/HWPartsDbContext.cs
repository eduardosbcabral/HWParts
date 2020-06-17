using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Enums;
using HWParts.Core.Infrastructure.Config;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace HWParts.Core.Infrastructure
{
    public class HWPartsDbContext : IdentityDbContext<Account>
    {
        public DbSet<ComponentBase> Components { get; set; }

        public DbSet<Processor> Processors { get; set; }
        public DbSet<Motherboard> Motherboards { get; set; }
        public DbSet<GraphicsCard> GraphicsCards { get; set; }
        public DbSet<Memory> Memories { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<PowerSupply> PowerSupplies { get; set; }
        public DbSet<Storage> Storages { get; set; }


        public HWPartsDbContext(DbContextOptions<HWPartsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ComponentBase>()
                .ToTable("TB_COMPONENTS")
                .Property(e => e.Platform)
                .HasConversion(
                    v => v.ToString(),
                    v => (EPlatform)Enum.Parse(typeof(EPlatform), v));

            //var typesToRegister = AppDomain.CurrentDomain
            //    .GetAssemblies()
            //    .SelectMany(x => x.GetTypes())
            //    .Where(x => typeof(IEntityMap).IsAssignableFrom(x) && !x.IsAbstract)
            //    .ToList();

            //foreach (var type in typesToRegister)
            //{
            //    dynamic instance = Activator.CreateInstance(type);
            //    modelBuilder.ApplyConfiguration(instance);
            //}
        }
    }
}
