using BlazorShopApp.Server.Data;
using BlazorShopApp.Shared;
using BlazorShopApp.Shared.DTO;
using Microsoft.EntityFrameworkCore;

namespace BlazorShopApp.Server.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly DataContext _dataContext;

        public CartService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ServiceResponse<List<CartProductDTO>>> GetCartProducts(List<CartItem> cartItems)
        {
            var result = new ServiceResponse<List<CartProductDTO>>
            {
                Data = new List<CartProductDTO>()
            };
            foreach (var cartItem in cartItems)
            {
                var product = await _dataContext.Products
                    .Where(x => x.Id == cartItem.ProductId)
                    .FirstOrDefaultAsync();

                if (product is null)
                {
                    continue;
                }

                var productVariant = await _dataContext.ProductVariants
                    .Where(x => x.ProductId == cartItem.ProductId && x.ProductTypeId == cartItem.ProductTypeId)
                    .Include(x => x.ProductType)
                    .FirstOrDefaultAsync();

                if (productVariant is null)
                {
                    continue;
                }

                var cartProduct = new CartProductDTO
                {
                    ProductId = product.Id,
                    Title = product.Title,
                    ImageUrl = product.ImageUrl,
                    Price = productVariant.Price,
                    ProductType = productVariant.ProductType.Name,
                    ProductTypeId = productVariant.ProductTypeId,
                    Quantity = cartItem.Quantity,

                };

                result.Data.Add(cartProduct);
            }
            return result;
        }
    }
}
