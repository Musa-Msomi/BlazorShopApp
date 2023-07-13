using BlazorShopApp.Shared;

namespace BlazorShopApp.Server.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<ServiceResponse<List<Category>>> GetCategoriesAsync();


    }
}
