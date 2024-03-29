﻿using BlazorShopApp.Server.Data;
using BlazorShopApp.Server.Services.ProductService;
using BlazorShopApp.Shared;
using BlazorShopApp.Shared.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorShopApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }



        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProducts()
        {
            var products = await _productService.GetProductsAsync();

            return Ok(products);

        }
        [HttpGet("featured")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetFeaturedProducts()
        {
            var products = await _productService.GetFeaturedProducts();

            return Ok(products);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _productService.GetProductAsync(id);
            return Ok(product);
        }

        [HttpGet("categories/{url}")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProductsByCategory(string url)
        {
            var products = await _productService.GetProductsByCategory(url);

            return Ok(products);
        }

        [HttpGet("search/{searchText}/{page}")]
        public async Task<ActionResult<ServiceResponse<ProductSearchResultDTO>>> SearchProducts(string searchText,int page = 1)
        {
            var products = await _productService.SearchProducts(searchText,page);

            return Ok(products);

        }

        [HttpGet("search-suggestions/{searchText}")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProductSearchSuggestions(string searchText)
        {
            var products = await _productService.GetProductsSearchSuggestions(searchText);

            return Ok(products);

        }
    }
}
