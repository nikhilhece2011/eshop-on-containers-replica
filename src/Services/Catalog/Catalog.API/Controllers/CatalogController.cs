using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Catalog.API.Data;
using Catalog.API.Domain;
using Catalog.API.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Catalog.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Catalog")]
    public class CatalogController : Controller
    {
        private readonly CatalogContext catalogContext;
        private IOptions<GlobalSettings> options;

        public CatalogController(CatalogContext _catalogContext,
                                 IOptions<GlobalSettings> _options)
        {
            catalogContext = _catalogContext;
            options = _options;
        }

        [HttpGet]
        [Route("CatalogTypes")]
        public async Task<IActionResult> CatalogTypes()
        {
            var items = await catalogContext.CatalogType.ToListAsync();
            return Ok(items);
        }

        [HttpGet]
        [Route("CatalogBrands")]
        public async Task<IActionResult> CatalogBrands()
        {
            var items = await catalogContext.CatalogBrand.ToListAsync();
            return Ok(items);
        }

        [HttpGet]
        [Route("items/{id:int}")]
        public async Task<IActionResult> GetItemById(int id)
        {
            if (id <= 0) return BadRequest();

            var item = catalogContext.CatalogItem.FirstOrDefault(m => m.Id == id);
            if (item == null)
                return NotFound();
            item.PictureUrl = item.PictureUrl.Replace("https://homepages.cae.wisc.edu/~ece533/images", options.Value.ExternalCatalogBaseUrl);
            return Ok(item);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Items([FromQuery] int pageSize = 5, [FromQuery] int pageIndex = 0)
        {
            var totalItems = await catalogContext.CatalogItem.LongCountAsync();

            var itemsOnPage = await catalogContext.CatalogItem.OrderBy(m => m.Name)
                                                .Skip(pageSize * pageIndex)
                                                .Take(pageSize)
                                                .ToListAsync();

            itemsOnPage = ChangeUrlPlaceHolder(itemsOnPage);
            var model = new PageniatedItemsViewModel<CatalogItem>(pageIndex, pageSize, totalItems, itemsOnPage);
            return Ok(model);
        }

        [HttpGet]
        [Route("[action]/withname/{name:minlength(1)}")]
        public async Task<IActionResult> Items(string name, [FromQuery] int pageSize = 5, [FromQuery] int pageIndex = 0)
        {
            var totalItems = await catalogContext.CatalogItem
                                    .Where(c => c.Name.StartsWith(name))
                                    .LongCountAsync();

            var itemsOnPage = await catalogContext.CatalogItem
                                                .Where(m => m.Name.StartsWith(name))
                                                .OrderBy(m => m.Name)
                                                .Skip(pageSize * pageIndex)
                                                .Take(pageSize)
                                                .ToListAsync();

            itemsOnPage = ChangeUrlPlaceHolder(itemsOnPage);
            var model = new PageniatedItemsViewModel<CatalogItem>(pageIndex, pageSize, totalItems, itemsOnPage);
            return Ok(model);
        }

        [HttpGet]
        [Route("[action]/type/{catalogTypeId}/brand/{catalogBrandId}")]
        public async Task<IActionResult> Items(int? catalogTypeId, int? catalogBrandId, [FromQuery] int pageSize = 5, [FromQuery] int pageIndex = 0)
        {
            var root = (IQueryable<CatalogItem>)catalogContext.CatalogItem;
            if (catalogTypeId.HasValue)
            {
                root = root.Where(m => m.CatalogTypeId == catalogTypeId.Value);
            }

            if (catalogBrandId.HasValue)
            {
                root = root.Where(m => m.CatalogBrandId == catalogBrandId.Value);
            }

            var totalItems = await root
                                    .LongCountAsync();

            var itemsOnPage = await root.OrderBy(m => m.Name)
                                                .Skip(pageSize * pageIndex)
                                                .Take(pageSize)
                                                .ToListAsync();

            itemsOnPage = ChangeUrlPlaceHolder(itemsOnPage);
            var model = new PageniatedItemsViewModel<CatalogItem>(pageIndex, pageSize, totalItems, itemsOnPage);
            return Ok(model);
        }

        [HttpPost]
        [Route("items")]
        public async Task<IActionResult> CreateProduct([FromBody] CatalogItem item)
        {
            catalogContext.CatalogItem.Add(item);
            await catalogContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetItemById), new { id = item.Id });
        }

        [HttpPut]
        [Route("items")]
        public async Task<IActionResult> UpdateProduct([FromBody] CatalogItem productToUpdate)
        {
            var catalogItem = await catalogContext.CatalogItem.FirstOrDefaultAsync(i => i.Id == productToUpdate.Id);

            if (catalogItem == null) return NotFound();
            catalogContext.CatalogItem.Update(catalogItem);

            await catalogContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetItemById), new { id = productToUpdate.Id });
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await catalogContext.CatalogItem.FirstOrDefaultAsync(m => m.Id == id);

            if (product == null) return NotFound();

            catalogContext.CatalogItem.Remove(product);

            await catalogContext.SaveChangesAsync();

            return NoContent();
        }



        private List<CatalogItem> ChangeUrlPlaceHolder(List<CatalogItem> items)
        {
            items.ForEach(x => x.PictureUrl = x.PictureUrl.Replace("https://homepages.cae.wisc.edu/~ece533/images", options.Value.ExternalCatalogBaseUrl));

            return items;
        }
    }
}
