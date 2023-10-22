using KantindenAl.App.Entity.Services;
using Microsoft.AspNetCore.Mvc;

namespace KantindenAl.App.MvcUI.ViewComponents
{
	public class CartModalViewComponent : ViewComponent
	{

		private readonly IAccountService _accountService;
		private readonly ICartService _cartService;
		private readonly IProductService _productService;

		public CartModalViewComponent(IAccountService accountService, ICartService cartService, IProductService productService)
		{
			_accountService = accountService;
			_cartService = cartService;
			_productService = productService;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var user = await _accountService.FindUserByUserNameAsync(User.Identity.Name);
			if(user != null)
			{
				var schoolId = HttpContext.Session.GetString("SchoolId");
				var cart = await _cartService.GetCart(user.Id, Convert.ToInt32(schoolId));
                var cartLines = await _cartService.GetCartLines(cart.Id);
                ViewBag.Cart = cart;
                return View(cartLines);
            }
			return View();
		}
	}
}
