using BlazorShopApp.Shared;

namespace BlazorShopApp.Client.Services.CategoryService
{
    public interface ICategoryService
    {
        List<Category> Categories { get; }
        Task  GetCategories();
    }
}
