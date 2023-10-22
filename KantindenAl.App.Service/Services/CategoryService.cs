using AutoMapper;
using KantindenAl.App.Entity.Entities;
using KantindenAl.App.Entity.Services;
using KantindenAl.App.Entity.UnitOfWork;
using KantindenAl.App.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantindenAl.App.Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CategoryViewModel>> GetAllAsync()
        {
            var list = await _unitOfWork.GetRepository<Category>().GetAllAsync();

            return _mapper.Map<List<CategoryViewModel>>(list);
        }

		public async Task<CategoryViewModel> GetCategoryById(Product product)
		{
			var category = await _unitOfWork.GetRepository<Category>().GetById(product.CategoryId);
            return _mapper.Map<CategoryViewModel>(category);
		}

        public async Task<CategoryViewModel> GetCategoryByProduct(ProductViewModel product)
        {
            var category = await _unitOfWork.GetRepository<Category>().Get(c => c.Id == product.CategoryId);
            return _mapper.Map<CategoryViewModel>(category);
        }
    }
}
