using KantindenAl.App.Entity.Entities;
using KantindenAl.App.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantindenAl.App.Entity.Services
{
    public interface ISaleService
    {
        Task<SaleViewModel> AddSale(CartViewModel cart, OwnerViewModel owner, UserViewModel user);
        Task AddSaleDetail(List<CartLineViewModel> cartLines, int saleId, int userId);
        Task<List<SaleViewModel>> GetSalesByUserId(int userId);
        Task<List<SaleViewModel>> GetSalesByOwnerId(int ownerId);
        Task<List<SaleDetailViewModel>> GetSaleDetailsBySaleId(int saleId);
        Task<SaleViewModel> GetSaleByReceiptNo(string receiptNo);
    }
}
