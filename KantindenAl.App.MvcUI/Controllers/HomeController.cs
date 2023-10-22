using Microsoft.AspNetCore.Mvc;

namespace KantindenAl.App.MvcUI.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		//Esat

		public IActionResult FAQ()
		{
			return View();
		}

		public IActionResult About()
		{
			return View();
		}

		public IActionResult Contact()
		{
			return View();
		}
	}
}
