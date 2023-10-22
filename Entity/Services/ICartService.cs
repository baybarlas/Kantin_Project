using KantindenAl.App.Entity.Entities;
using KantindenAl.App.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantindenAl.App.Entity.Services
{
    public interface ICartService
    {

		Task<CartViewModel> GetCart(int id, int schoolId);

		Task<List<CartLineViewModel>> GetCartLines(int id);

        Task<List<CartLineViewModel>> GetCartLineByProductId(int id);

        Task AddProductToCartLine(int id,int quantity, CartViewModel cart);

        Task ClearCart(CartViewModel cart);

        Task DeleteCartLine(CartLineViewModel cartLine);

		Task RemoveFromCart(int id, CartViewModel cart);

        //Task<List<CartLineViewModel>> AddProductsToCart(List<CartLineViewModel> cartLine, CartViewModel cart);

	}
}
