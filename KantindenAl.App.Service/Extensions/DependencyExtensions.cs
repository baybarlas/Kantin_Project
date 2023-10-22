using KantindenAl.App.DataAccess.Contexts;
using KantindenAl.App.DataAccess.Identity;
using KantindenAl.App.DataAccess.Repositories;
using KantindenAl.App.DataAccess.UnitOfWork;
using KantindenAl.App.Entity.Repositories;
using KantindenAl.App.Entity.Services;
using KantindenAl.App.Entity.UnitOfWork;
using KantindenAl.App.Service.Mapping;
using KantindenAl.App.Service.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantindenAl.App.Service.Extensions
{
	public static class DependencyExtensions
	{
		public static void AddExtensions(this IServiceCollection services)
		{
			services.AddIdentity<AppUser, AppRole>(
			opt =>
			{
				opt.Password.RequireNonAlphanumeric = false;
				opt.Password.RequiredLength = 3;
				opt.Password.RequireUppercase = false;
				opt.Password.RequireLowercase = false;
				opt.Password.RequireDigit = false;

				//opt.User.AllowedUserNameCharacters = "abcdefghijklmnoprstuvyzqw0123456789";

				opt.User.RequireUniqueEmail = true;

				opt.Lockout.MaxFailedAccessAttempts = 3;    //default : 5
				opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1); //default 5 dk
			}
			).AddEntityFrameworkStores<KantindenAlDbContext>();



			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IProductService, ProductService>();
			services.AddScoped<ICategoryService, CategoryService>();
			services.AddScoped<IAccountService, AccountService>();
			services.AddScoped<ISchoolService, SchoolService>();
			services.AddScoped<IOwnerService, OwnerService>();
			services.AddScoped<IStudentService, StudentService>();
			services.AddScoped<ICartService, CartService>();
			services.AddScoped<IWalletActivityService, WalletActivityService>();
			services.AddScoped<ISaleService, SaleService>();


			

			services.AddAutoMapper(typeof(MappingProfile));
		}
	}
}
