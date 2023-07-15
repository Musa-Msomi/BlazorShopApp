using BlazorShopApp.Shared;

namespace BlazorShopApp.Client.Services.ProductService
{
    public interface IProductService
    {
        string Message { get; set; }
        event Action ProductsChanged;
        int CurrentPage { get; set; }
        int PageCount { get; set; }
        string LastSearchText { get; set; }
        List<Product> Products { get; set; }
        Task GetProducts(string? categoryUrl = null);
        Task<ServiceResponse<Product>> GetProduct(int id);
        Task SearchProducts(string searchText, int page);
        Task<List<string>> GetProductSearchSuggestions(string searchText);


    }
}
