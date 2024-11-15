using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyShop.Domain.Models;
using MyShop.Infrastructure;

namespace MyShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        public ProductsController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        //GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _uow.ProductRepository.AllAsync();
            return products.ToList();
        }

        //GET: api/Products/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _uow.ProductRepository.GetAsync(id);
            if(product == null)
            {
                return NotFound();
            }
            return product;
        }

        //POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _uow.ProductRepository.Add(product);
            await _uow.SaveAsync();

            return CreatedAtAction("GetProduct", new { id = product.ProductID }, product);
        }
    }
}
