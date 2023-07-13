using BlazorShopApp.Server.Data;
using BlazorShopApp.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorShopApp.Server.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _dataContext;

        public CategoryService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ServiceResponse<List<Category>>> GetCategoriesAsync()
        {
            var categories = await _dataContext.Categories.ToListAsync();
            var response = new ServiceResponse<List<Category>>
            {
                Data = categories
            };

            return response;
        }
    }
}
