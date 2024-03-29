﻿using BlazorShopApp.Shared;
using BlazorShopApp.Shared.DTO;

namespace BlazorShopApp.Server.Services.CartService
{
    public interface ICartService
    {
        Task<ServiceResponse<List<CartProductDTO>>> GetCartProducts(List<CartItem> cartItems);
        Task<ServiceResponse<List<CartProductDTO>>> StoreCartItems(List<CartItem> cartItems, int userId);

    }
}
