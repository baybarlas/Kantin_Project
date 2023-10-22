using KantindenAl.App.Entity.Entities;
using KantindenAl.App.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantindenAl.App.Entity.Services
{
	public interface IProductService
	{
		Task<IEnumerable<ProductViewModel>> GetAllProductsBySchoolId(int schoolId);

		Task<List<ProductViewModel>> GetSearchedProductsBySchoolId(string search, int schoolId);

		Task<List<ProductViewModel>> GetProductsByCategoryAndSchoolId(string id, int schoolId);

		Task AddProduct(ProductViewModel product);

		Task<ProductViewModel> GetProductById(string id);

		Task<List<ProductViewModel>> GetProductsByCartLine(List<CartLineViewModel> cartLines);

		Task UpdateProduct(ProductViewModel model);
	}
}
