using AutoMapper;
using KantindenAl.App.DataAccess.Identity;
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
    public class SaleService : ISaleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;


        public SaleService(IUnitOfWork unitOfWork, ICartService cartService, IProductService productService, IAccountService accountService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _cartService = cartService;
            _productService = productService;
            _accountService = accountService;
            _mapper = mapper;
        }

        public async Task<SaleViewModel> AddSale(CartViewModel cart, OwnerViewModel owner, UserViewModel user)
        {
            var sale = new Sale()
            {
                
                TotalAmount = cart.TotalAmount,
                CreatedDate = DateTime.Now,
                OwnerId = owner.Id,
                UserId = user.Id,
                ReceiptNo = "RCPT" + cart.Id
            };
            owner.Balance += cart.TotalAmount;
            user.Balance -= cart.TotalAmount;
            await _accountService.UpdateOwner(owner);
            await _accountService.UpdateUser(user);
            await _unitOfWork.GetRepository<Sale>().Add(sale);
            await _unitOfWork.CommitAsync();
            var activityOwner = new WalletActivity()
            {
                Date = DateTime.Now,
                UserId = owner.Id,
                NewBalance = owner.Balance,
                TotalAmount = cart.TotalAmount,
                receiptNo = sale.ReceiptNo,
                Type = "Satış"
                
            };
            var activityParent = new WalletActivity()
            {
                Date = DateTime.Now,
                UserId = user.Id,
                NewBalance = user.Balance,
                TotalAmount = cart.TotalAmount,
                receiptNo = sale.ReceiptNo,
                Type = "Alışveriş",
                
            };
            await _unitOfWork.GetRepository<WalletActivity>().Add(activityOwner);
            await _unitOfWork.GetRepository<WalletActivity>().Add(activityParent);
            await _unitOfWork.CommitAsync();
            var cartLines = await _cartService.GetCartLines(cart.Id);
            await this.AddSaleDetail(cartLines, sale.Id, user.Id);
            await _cartService.ClearCart(cart);
            return _mapper.Map<SaleViewModel>(sale);
        }

        public async Task AddSaleDetail(List<CartLineViewModel> cartLines, int saleId, int userId)
        {
            var product = new Product();
            foreach (var cartLine in cartLines)
            {
                var saleDetail = new SaleDetail()
                {
                    SaleId = saleId,
                    ProductId = cartLine.ProductId,
                    Quantity = cartLine.Quantity,
                    UserId = userId,
                };
                
                await _unitOfWork.GetRepository<SaleDetail>().Add(saleDetail);
                var newCartLine = await _unitOfWork.GetRepository<CartLine>().Get(cl => cl.Id == cartLine.Id);
                newCartLine.IsDeleted = true;
                product = await _unitOfWork.GetRepository<Product>().Get(p => p.Id == Convert.ToInt32(cartLine.Product.Id));
                product.Stock -= cartLine.Quantity;
                _unitOfWork.GetRepository<Product>().Update(product);
                await _unitOfWork.CommitAsync();
                _unitOfWork.GetRepository<CartLine>().Update(newCartLine);
                await _unitOfWork.CommitAsync();
            }
            
        }

        public async Task<List<SaleViewModel>> GetSalesByOwnerId(int ownerId)
        {
            var sale = await _unitOfWork.GetRepository<Sale>().GetAll(s => s.OwnerId == ownerId && s.IsDeleted == false);
            return _mapper.Map<List<SaleViewModel>>(sale);
        }

        public async Task<List<SaleViewModel>> GetSalesByUserId(int userId)
        {
            var sale = await _unitOfWork.GetRepository<Sale>().GetAll(s => s.UserId == userId && s.IsDeleted == false);
            return _mapper.Map<List<SaleViewModel>>(sale);
        }

        public async Task<List<SaleDetailViewModel>> GetSaleDetailsBySaleId(int saleId)
        {
            var saleDetails = await _unitOfWork.GetRepository<SaleDetail>().GetAll(sd => sd.SaleId == saleId && sd.IsDeleted == false, null, sd => sd.Product);
            return _mapper.Map<List<SaleDetailViewModel>>(saleDetails);
        }

        public async Task<SaleViewModel> GetSaleByReceiptNo(string receiptNo)
        {
            var sale = await _unitOfWork.GetRepository<Sale>().Get(s => s.ReceiptNo == receiptNo && s.IsDeleted == false);
            return _mapper.Map<SaleViewModel>(sale);
        }
    }
}
