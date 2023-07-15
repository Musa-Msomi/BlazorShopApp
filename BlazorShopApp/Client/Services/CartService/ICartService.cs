using BlazorShopApp.Shared;
using BlazorShopApp.Shared.DTO;

namespace BlazorShopApp.Client.Services.CartService
{
    public interface ICartService
    {
        event Action OnChange;
        Task AddToCart(CartItem cartItem);
        Task<List<CartItem>> GetAllCartItems();
        Task<List<CartProductDTO>> GetCartProducts();
        Task RemoveProductFromCart(int productId, int productTypeId);
    }
}
