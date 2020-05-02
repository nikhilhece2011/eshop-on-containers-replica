using Catalog.API.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public class CatalogSeed
    {
        public static async Task SeedAsync(CatalogContext context)
        {
            context.Database.Migrate();
            if (!context.CatalogBrand.Any())
            {
                context.CatalogBrand.AddRange(GetCatalogBrands());
                await context.SaveChangesAsync();
            }
            if (!context.CatalogType.Any())
            {
                context.CatalogType.AddRange(GetCatalogTypes());
                await context.SaveChangesAsync();
            }
            if (!context.CatalogItem.Any())
            {
                context.CatalogItem.AddRange(GetCatalogItems());
                await context.SaveChangesAsync();
            }
        }

        public static IEnumerable<CatalogBrand> GetCatalogBrands()
        {
            return new List<CatalogBrand>()
            {
                new CatalogBrand(){Brand = "Adidas"},
                new CatalogBrand(){Brand = "Puma"},
                new CatalogBrand(){Brand = "Nike"},
            };
        }

        public static IEnumerable<CatalogType> GetCatalogTypes()
        {
            return new List<CatalogType>()
            {
                new CatalogType(){Type  ="Running"},
                new CatalogType(){Type = "Hiking"},
                new CatalogType(){Type = "Tennis"},
            };
        }

        public static IEnumerable<CatalogItem> GetCatalogItems()
        {
            return new List<CatalogItem>()
            {
                new CatalogItem(){ CatalogBrandId = 1, CatalogTypeId=1,Description = "Shoes 1",Name = "Name 1",Price = 199.99M, PictureUrl = "https://homepages.cae.wisc.edu/~ece533/images/shoes-1.png"},
                new CatalogItem(){ CatalogBrandId = 2, CatalogTypeId=1,Description = "Shoes 2",Name = "Name 2",Price = 100.99M, PictureUrl = "https://homepages.cae.wisc.edu/~ece533/images/shoes-2.png"},
                new CatalogItem(){ CatalogBrandId = 3, CatalogTypeId=1,Description = "Shoes 3",Name = "Name 3",Price = 109.99M, PictureUrl = "https://homepages.cae.wisc.edu/~ece533/images/shoes-3.png"},
                new CatalogItem(){ CatalogBrandId = 1, CatalogTypeId=2,Description = "Shoes 4",Name = "Name 4",Price = 19.99M, PictureUrl = "https://homepages.cae.wisc.edu/~ece533/images/shoes-4.png"},
                new CatalogItem(){ CatalogBrandId = 1, CatalogTypeId=3,Description = "Shoes 5",Name = "Name 5",Price = 9.99M,PictureUrl = "https://homepages.cae.wisc.edu/~ece533/images/shoes-5.png"},
                new CatalogItem(){ CatalogBrandId = 2, CatalogTypeId=2,Description = "Shoes 6",Name = "Name 6",Price = 245.99M,PictureUrl = "https://homepages.cae.wisc.edu/~ece533/images/shoes-6.png"},
                new CatalogItem(){ CatalogBrandId = 3, CatalogTypeId=3,Description = "Shoes 7",Name = "Name 7",Price = 390.99M,PictureUrl = "https://homepages.cae.wisc.edu/~ece533/images/shoes-7.png"},
                new CatalogItem(){ CatalogBrandId = 3, CatalogTypeId=1,Description = "Shoes 8",Name = "Name 8",Price = 247.99M,PictureUrl = "https://homepages.cae.wisc.edu/~ece533/images/shoes-8.png"},
                new CatalogItem(){ CatalogBrandId = 3, CatalogTypeId=1,Description = "Shoes 9",Name = "Name 9",Price = 219.99M,PictureUrl = "https://homepages.cae.wisc.edu/~ece533/images/shoes-9.png"},
                new CatalogItem(){ CatalogBrandId = 3, CatalogTypeId=2,Description = "Shoes 10",Name = "Name 10",Price = 102.99M,PictureUrl = "https://homepages.cae.wisc.edu/~ece533/images/shoes-10.png"}
            };
        }


    }
}
