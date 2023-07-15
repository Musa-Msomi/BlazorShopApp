using BlazorShopApp.Server.Data;
using BlazorShopApp.Shared;
using BlazorShopApp.Shared.DTO;
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

        public async Task<ServiceResponse<List<Product>>> GetFeaturedProducts()
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await _dataContext.Products
                .Where(x => x.IsFeatured)
                .Include(x => x.Variants)
                .ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<Product>> GetProductAsync(int productId)
        {
            var response = new ServiceResponse<Product>();
            var product = await _dataContext.Products
                .Include(x => x.Variants)
                .ThenInclude(x => x.ProductType)
                .FirstOrDefaultAsync(x => x.Id == productId);

            if (product is null)
            {
                response.Success = false;
                response.Message = "Sorry, this product is currently unavailable";
            }
            else
            {
                response.Data = product;
            }

            return response;
        }

        public async Task<ServiceResponse<List<Product>>> GetProductsAsync()
        {
            var response = new ServiceResponse<List<Product>>()
            {
                Data = await _dataContext.Products
                .Include(x => x.Variants)
                .ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<List<Product>>> GetProductsByCategory(string url)
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await _dataContext.Products
                .Where(x => x.Category.Url.ToLower() == url.ToLower())
                .Include(_ => _.Variants)
                .ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<List<string>>> GetProductsSearchSuggestions(string searchText)
        {
            var products = await FindProductsBySearchText(searchText);

            List<string> result = new List<string>();

            foreach (var product in products)
            {
                if (product.Title.ToLower().Contains(searchText.ToLower()))
                {
                    result.Add(product.Title);
                }

                if (product.Description is not null)
                {
                    var punctuation = product.Description.Where(char.IsPunctuation)
                        .Distinct()
                        .ToArray();

                    var words = product.Description.Split()
                        .Select(x => x.Trim(punctuation));

                    foreach (var word in words)
                    {
                        if (word.ToLower().Contains(searchText.ToLower()) && !result.Contains(word))
                        {
                            result.Add(word);
                        }
                    }
                }

            }
            return new ServiceResponse<List<string>> { Data = result };
        }

        public async Task<ServiceResponse<ProductSearchResultDTO>> SearchProducts(string searchText, int page)
        {

            var pageResults = 2f;
            var pageCount = Math.Ceiling((await FindProductsBySearchText(searchText)).Count / pageResults);

            var products = await _dataContext.Products
                            .Where(x => x.Title.ToLower().Contains(searchText.ToLower()) || x.Description.ToLower().Contains(searchText.ToLower()))
                            .Include(_ => _.Variants)
                            .Skip((page - 1) * (int)pageResults)
                            .Take((int)pageResults)
                            .ToListAsync();
            // search description and title of product for matching searchText
            var response = new ServiceResponse<ProductSearchResultDTO>
            {
                Data = new ProductSearchResultDTO
                {
                    Products = products,
                    CurrentPage = page,
                    Pages = (int)pageCount
                }
            };

            return response;
        }

        private async Task<List<Product>> FindProductsBySearchText(string searchText)
        {
            return await _dataContext.Products
                            .Where(x => x.Title.ToLower().Contains(searchText.ToLower()) || x.Description.ToLower().Contains(searchText.ToLower()))
                            .Include(_ => _.Variants)
                            .ToListAsync();
        }
    }
}

