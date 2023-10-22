using AutoMapper;
using KantindenAl.App.Entity.Entities;
using KantindenAl.App.Entity.Services;
using KantindenAl.App.Entity.UnitOfWork;
using KantindenAl.App.Entity.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantindenAl.App.Service.Services
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly IAccountService _accountService;
		public CartService(IUnitOfWork unitOfWork, IMapper mapper, IProductService productService, IAccountService accountService)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_productService = productService;
			_accountService = accountService;
		}



		public async Task AddProductToCartLine(int id, int quantity, CartViewModel cart)
        {
            var cartLines = await _unitOfWork.GetRepository<CartLine>().GetAll(cl => cl.CartId == cart.Id && cl.IsDeleted == false);
            if (cartLines.Any(cl => cl.ProductId == id && cl.IsDeleted == false))
            {
                foreach (var cartLine in cartLines)
                {
                    if (cartLine.ProductId == id)
                    {
                        cartLine.Quantity += quantity;
                        if(cartLine.Quantity == 0)
                        {
                            cartLine.IsDeleted = true;
                        }
                        _unitOfWork.GetRepository<CartLine>().Update(cartLine);
                        await _unitOfWork.CommitAsync();
                    }
                }
            }
            else
            {
                var cartLine = new CartLine()
                {
                    CartId = cart.Id,
                    Quantity = quantity,
                    ProductId = id
                };
                await _unitOfWork.GetRepository<CartLine>().Add(cartLine);
            }

            var product = await _productService.GetProductById(id.ToString());
            cart.TotalAmount += product.UnitPrice*quantity;
            _unitOfWork.GetRepository<Cart>().Update(_mapper.Map<Cart>(cart));
            await _unitOfWork.CommitAsync();
        }

        //public Task<List<CartLineViewModel>> AddProductsToCart(List<CartLineViewModel> cartLine, CartViewModel cart)
        //{


        //}

        public async Task<CartViewModel> GetCart(int id, int schoolId)
        {
            
            var cart = await _unitOfWork.GetRepository<Cart>().Get(c => c.UserId == id && c.IsDeleted == false);
            if (cart == null)
            {
                cart = new Cart()
                {
                    UserId = id,
                    SchoolId = schoolId,
                    CreatedDate = DateTime.Now
                };
                await _unitOfWork.GetRepository<Cart>().Add(cart);
                await _unitOfWork.CommitAsync();
            }
            return _mapper.Map<CartViewModel>(cart);
        }

        public async Task<List<CartLineViewModel>> GetCartLines(int id)
        {
            var list = await _unitOfWork.GetRepository<CartLine>().GetAll(cl => cl.CartId == id && cl.IsDeleted == false, null, p => p.Product);
            return _mapper.Map<List<CartLineViewModel>>(list);
        }

        public async Task<List<CartLineViewModel>> GetCartLineByProductId(int id)
        {
            var cartline = await _unitOfWork.GetRepository<CartLine>().Get(c => c.ProductId == id && c.IsDeleted == false);
            return _mapper.Map<List<CartLineViewModel>>(cartline);
        }

        public async Task ClearCart(CartViewModel cart)
        {
            _unitOfWork.GetRepository<Cart>().Delete(_mapper.Map<Cart>(cart));
            await _unitOfWork.CommitAsync();
        }


        public async Task RemoveFromCart(int id, CartViewModel cart)
        {
            var cartLine = await _unitOfWork.GetRepository<CartLine>().Get(cl => cl.CartId == cart.Id && cl.IsDeleted == false, p => p.Product);
			cart.TotalAmount -= (cartLine.Product.UnitPrice * cartLine.Quantity);
            _unitOfWork.GetRepository<Cart>().Update(_mapper.Map<Cart>(cart));
			_unitOfWork.GetRepository<CartLine>().Delete(cartLine);
            await _unitOfWork.CommitAsync();

        }

        public async Task DeleteCartLine(CartLineViewModel cartLine)
        {
            var line = await _unitOfWork.GetRepository<CartLine>().Get(cl => cl.Id == cartLine.Id && cl.IsDeleted == false);
            _unitOfWork.GetRepository<CartLine>().Delete(line);
        }
    }
}
