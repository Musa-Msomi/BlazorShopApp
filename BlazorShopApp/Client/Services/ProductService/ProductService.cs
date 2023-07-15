using BlazorShopApp.Shared;
using System.Net.Http.Json;

namespace BlazorShopApp.Client.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<Product> Products { get; set; } = new List<Product>();
        public string Message { get; set; } = "loading products...";

        public event Action ProductsChanged;

        public async Task<ServiceResponse<Product>> GetProduct(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<Product>>($"api/products/{id}");
            return result;
        }

        public async Task GetProducts(string? categoryUrl = null)
        {

            var result = categoryUrl == null ? await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/products/featured")
                : await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/products/categories/{categoryUrl}");
            if (result is not null && result.Data is not null)
            {
                Products = result.Data;
            }
            ProductsChanged.Invoke();

        }

        public async Task<List<string>> GetProductSearchSuggestions(string searchText)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<string>>>($"api/products/search-suggestions/{searchText}");

            return result.Data;
        }

        public async Task SearchProducts(string searchText)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/products/search/{searchText}");

            if (result is not null && result.Data is not null)
            {
                Products = result.Data;
            }

            if(Products.Count == 0)
            {
                Message = "No product found";
            }

            ProductsChanged?.Invoke();  
        }
    }
}
