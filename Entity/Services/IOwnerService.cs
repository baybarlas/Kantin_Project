using KantindenAl.App.Entity.Entities;
using KantindenAl.App.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantindenAl.App.Entity.Services
{
    public interface IOwnerService
    {
        Task<string> AddProduct(ProductViewModel product);

        OwnerViewModel GetOwnerBySchoolId(string id);
    }
}
