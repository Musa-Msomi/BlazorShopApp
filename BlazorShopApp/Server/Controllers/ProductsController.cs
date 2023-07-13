using BlazorShopApp.Server.Data;
using BlazorShopApp.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorShopApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public ProductsController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProducts()
        {
            var products = await _dataContext.Products.ToListAsync();
            var response = new ServiceResponse<List<Product>>()
            {
                Data = products
            };
            return Ok(response);

        }
    }
}
