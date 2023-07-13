﻿using BlazorShopApp.Server.Data;
using BlazorShopApp.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorShopApp.Server.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly DataContext _dataContext;

        public ProductService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ServiceResponse<List<Product>>> GetProductsAsync()
        {
            var response = new ServiceResponse<List<Product>>()
            {
                Data = await _dataContext.Products.ToListAsync()
            };

            return response;
        }
    }
}
