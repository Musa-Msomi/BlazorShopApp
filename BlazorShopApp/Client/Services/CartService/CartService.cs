using Blazored.LocalStorage;
using BlazorShopApp.Shared;
using BlazorShopApp.Shared.DTO;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace BlazorShopApp.Client.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _provider;

        public CartService(ILocalStorageService localStorageService, HttpClient httpClient, AuthenticationStateProvider provider)
        {
            _localStorageService = localStorageService;
            _httpClient = httpClient;
            _provider = provider;
        }

        public event Action OnChange;

        public async Task AddToCart(CartItem cartItem)
        {
            if((await _provider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated)
            {
                Console.WriteLine("User authenticated");
            } else
            {
                Console.WriteLine("User is not authenticated");
            }
            var cart = await _localStorageService.GetItemAsync<List<CartItem>>("cart");
            if (cart is null)
            {
                cart = new List<CartItem>();
            }
            // checking if same item is being added and increasing quantity. i.e buying two of the same product
            var sameItem = cart.Find(x => x.ProductId == cartItem.ProductId && x.ProductTypeId == cartItem.ProductTypeId);
            if (sameItem is null)
            {
                cart.Add(cartItem);
            }
            else
            {
                sameItem.Quantity += cartItem.Quantity;
            }


            await _localStorageService.SetItemAsync("cart", cart);
            OnChange.Invoke();
        }

        public async Task<List<CartItem>> GetAllCartItems()
        {
            var cart = await _localStorageService.GetItemAsync<List<CartItem>>("cart");
            if (cart is null)
            {
                cart = new List<CartItem>();
            }
            return cart;
        }

        public async Task<List<CartProductDTO>> GetCartProducts()
        {
            var cartItems = await _localStorageService.GetItemAsync<List<CartItem>>("cart");
            var response = await _httpClient.PostAsJsonAsync("api/carts/products", cartItems);
            var cartProducts = await response.Content.ReadFromJsonAsync<ServiceResponse<List<CartProductDTO>>>();

            return cartProducts.Data;

        }

        public async Task RemoveProductFromCart(int productId, int productTypeId)
        {
            var cartItems = await _localStorageService.GetItemAsync<List<CartItem>>("cart");

            if (cartItems is null)
            {
                return;
            }

            var cartItem = cartItems.Find(x => x.ProductId == productId && x.ProductTypeId == productTypeId);

            if (cartItem is not null)
            {
                cartItems.Remove(cartItem);
                // save remaining items to local storage again
                await _localStorageService.SetItemAsync("cart", cartItems);
                OnChange.Invoke();
            }

        }

        public async Task UpdateQuantity(CartProductDTO cartProduct)
        {
            var cartItems = await _localStorageService.GetItemAsync<List<CartItem>>("cart");

            if (cartItems is null)
            {
                return;
            }

            var cartItem = cartItems.Find(x => x.ProductId == cartProduct.ProductId && x.ProductTypeId == cartProduct.ProductTypeId);

            if (cartItem is not null)
            {
                cartItem.Quantity = cartProduct.Quantity;
                // save remaining items to local storage again
                await _localStorageService.SetItemAsync("cart", cartItems);

            }
        }
    }
}
