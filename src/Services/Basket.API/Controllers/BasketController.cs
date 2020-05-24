using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Basket.API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Basket.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class BasketController : Controller
    {
        private ICartRepository _repository;
        private ILogger _logger;

        public BasketController(ICartRepository repository, ILoggerFactory factory)
        {
            _repository = repository;
            //_logger = factory.CreateLogger<BasketController>();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Cart), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(string id)
        {
            var basket = await _repository.GetCartAsync(id);

            return Ok(basket);
        }

        [HttpPost]

        [ProducesResponseType(typeof(Cart), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post([FromBody]Cart value)
        {
            var basket = await _repository.UpdateCartAsync(value);

            return Ok(basket);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _logger.LogInformation("Delete method in Cart controller reached");
            _repository.DeleteCartAsync(id);


        }
    }
}