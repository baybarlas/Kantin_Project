using AutoMapper;
using KantindenAl.App.DataAccess.Identity;
using KantindenAl.App.Entity.Entities;
using KantindenAl.App.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantindenAl.App.Service.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Product, ProductViewModel>().ReverseMap();
			CreateMap<Category, CategoryViewModel>().ReverseMap();
			CreateMap<AppRole, RoleViewModel>().ReverseMap();
			CreateMap<AppUser, UserViewModel>().ReverseMap();
			CreateMap<AppUser, OwnerViewModel>().ReverseMap();
			CreateMap<School, SchoolViewModel>().ReverseMap();
			CreateMap<Student, StudentViewModel>().ReverseMap();
			CreateMap<AppUser, UserBillingInformationViewModel>().ReverseMap();
			CreateMap<WalletActivity, WalletActivityViewModel>().ReverseMap();
			CreateMap<Cart, CartViewModel>().ReverseMap();
			CreateMap<CartLine, CartLineViewModel>().ReverseMap();
			CreateMap<Sale, SaleViewModel>().ReverseMap();
			CreateMap<SaleDetail, SaleDetailViewModel>().ReverseMap();
		}
	}
}
