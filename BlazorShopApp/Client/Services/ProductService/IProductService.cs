using BlazorShopApp.Shared;

namespace BlazorShopApp.Client.Services.ProductService
{
    public interface IProductService
    {
        string Message { get; set; }
        event Action ProductsChanged;
        List<Product> Products { get; set; }
        Task GetProducts(string? categoryUrl = null);
        Task<ServiceResponse<Product>> GetProduct(int id);
        Task SearchProducts(string searchText);
        Task<List<string>> GetProductSearchSuggestions(string searchText);


    }
}
