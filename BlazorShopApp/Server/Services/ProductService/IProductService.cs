﻿using BlazorShopApp.Shared;

namespace BlazorShopApp.Server.Services.ProductService
{
    public interface IProductService
    {
        Task<ServiceResponse<List<Product>>> GetProductsAsync();
        Task<ServiceResponse<Product>> GetProductAsync(int productId);

        Task<ServiceResponse<List<Product>>> GetProductsByCategory(string url);
        Task<ServiceResponse<List<Product>>> SearchProducts(string searchText);
    }
}
