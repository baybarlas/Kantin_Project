using KantindenAl.App.Entity.Entities;
using KantindenAl.App.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantindenAl.App.Entity.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryViewModel>> GetAllAsync();

		Task<CategoryViewModel> GetCategoryById(Product product);

        Task<CategoryViewModel> GetCategoryByProduct(ProductViewModel product);

	}
}
