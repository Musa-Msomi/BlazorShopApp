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

        List<Product> Products { get; set; } = new List<Product>();

        public async Task GetProducts()
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/products");
            if (result is not null && result.Data is not null)
            {
                Products = result.Data;
            }

        }
    }
}
