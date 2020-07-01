using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Enums;
using HWParts.Core.Infrastructure.Config;
using HWParts.Core.Infrastructure.Config.Maps;
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
        public DbSet<ComponentPrice> ComponentsPrices { get; set; }

        public DbSet<Account> Accounts { get; set; }


        public HWPartsDbContext(DbContextOptions<HWPartsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ComponentBaseMap());
            modelBuilder.ApplyConfiguration(new ComponentPriceMap());
        }
    }
}
