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
	public class ProductService : IProductService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

        public async Task AddProduct(ProductViewModel product)
        {
            await _unitOfWork.GetRepository<Product>().Add(_mapper.Map<Product>(product));
			_unitOfWork.Commit();
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllProductsBySchoolId(int schoolId)
		{
			var list = await _unitOfWork.GetRepository<Product>().GetAll(p => p.SchoolId == schoolId);
			return _mapper.Map<IEnumerable<ProductViewModel>>(list.Where(x => x.IsDeleted == false));
		}

		public async Task<ProductViewModel> GetProductById(string id)
		{
			var product = await _unitOfWork.GetRepository<Product>().Get(p => p.Id == Convert.ToInt32(id) && p.IsDeleted == false);
			return _mapper.Map<ProductViewModel>(product);
		}

		public async Task<List<ProductViewModel>> GetProductsByCartLine(List<CartLineViewModel> cartLines)
		{
			var products = new List<Product>();
			foreach (var cartLine in cartLines)
			{
				products.Add(await _unitOfWork.GetRepository<Product>().Get(p => p.Id == cartLine.ProductId && p.IsDeleted == false));
			}
			return _mapper.Map<List<ProductViewModel>>(products);
		}

		public async Task<List<ProductViewModel>> GetProductsByCategoryAndSchoolId(string id, int schoolId)
        {
			var list = await _unitOfWork.GetRepository<Product>().GetAll(p => p.CategoryId.ToString() == id && p.IsDeleted == false && p.SchoolId == schoolId);
			return _mapper.Map<List<ProductViewModel>>(list.Where(x => x.IsDeleted == false));
		}

		public async Task<List<ProductViewModel>> GetSearchedProductsBySchoolId(string search, int schoolId)
		{
			var list = await _unitOfWork.GetRepository<Product>().GetAll(p => p.SchoolId == schoolId && p.Name.ToLower().Contains(search.ToLower()) && p.IsDeleted == false  );
			return _mapper.Map<List<ProductViewModel>>(list.Where(x => x.IsDeleted == false));
		}

        public async Task UpdateProduct(ProductViewModel model)
        {
            var product = await _unitOfWork.GetRepository<Product>().Get(p => p.Id == model.Id);
			product.Name = model.Name;
			product.Stock = model.Stock;
			product.UnitPrice = model.UnitPrice;
			product.Description = model.Description;
			product.ImageUrl = model.ImageUrl;
			product.CategoryId = model.CategoryId;
			_unitOfWork.GetRepository<Product>().Update(product);
			await _unitOfWork.CommitAsync();
        }
    }
}
