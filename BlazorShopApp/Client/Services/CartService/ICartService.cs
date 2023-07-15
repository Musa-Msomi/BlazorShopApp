using BlazorShopApp.Shared;

namespace BlazorShopApp.Client.Services.CartService
{
    public interface ICartService
    {
        event Action OnChange;
        Task AddToCart(CartItem cartItem);
        Task<List<CartItem>> GetAllCartItems();
    }
}
