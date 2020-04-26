using Catalog.API.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<CatalogItem> CatalogItem { get; set; }
        public DbSet<CatalogBrand> CatalogBrand { get; set; }
        public DbSet<CatalogType> CatalogType { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CatalogBrand>(ConfigureCatalogBrand);
            builder.Entity<CatalogType>(ConfigureCatalogType);
            builder.Entity<CatalogItem>(ConfigureCatalogItem);
        }

        private void ConfigureCatalogItem(EntityTypeBuilder<CatalogItem> obj)
        {
            obj.Property(m => m.Id);
            obj.HasOne(m => m.CatalogBrand)
                .WithMany()
                .HasForeignKey(m => m.CatalogBrandId);
            obj.HasOne(m => m.CatalogType)
                .WithMany()
                .HasForeignKey(m => m.CatalogTypeId);
        }

        private void ConfigureCatalogType(EntityTypeBuilder<CatalogType> obj)
        {
            obj.Property(m => m.Id);
        }

        private void ConfigureCatalogBrand(EntityTypeBuilder<CatalogBrand> obj)
        {
            obj.Property(m => m.Id);
        }
    }
}
