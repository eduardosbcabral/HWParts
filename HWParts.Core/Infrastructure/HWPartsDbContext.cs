using HWParts.Core.Domain.Entities;
using HWParts.Core.Infrastructure.Config;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;

namespace HWParts.Core.Infrastructure
{
    public class HWPartsDbContext : IdentityDbContext<Account>
    {
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

            modelBuilder.Entity<Case>()
                .ToTable("TB_CASES")
                .HasKey(x => x.Id);

            modelBuilder.Entity<GraphicsCard>()
                .ToTable("TB_GRAPHICS_CARDS")
                .HasKey(x => x.Id);

            modelBuilder.Entity<Memory>()
                .ToTable("TB_MEMORIES")
                .HasKey(x => x.Id);

            modelBuilder.Entity<Motherboard>()
               .ToTable("TB_MOTHERBOARDS")
               .HasKey(x => x.Id);

            modelBuilder.Entity<PowerSupply>()
               .ToTable("TB_POWER_SUPPLIES")
               .HasKey(x => x.Id);

            modelBuilder.Entity<Processor>()
              .ToTable("TB_PROCESSORS")
              .HasKey(x => x.Id);

            modelBuilder.Entity<Storage>()
              .ToTable("TB_STORAGE")
              .HasKey(x => x.Id);

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
