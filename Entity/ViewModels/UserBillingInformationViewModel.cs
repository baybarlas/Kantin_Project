using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KantindenAl.App.Entity.ViewModels
{
    public class UserBillingInformationViewModel
    {
        public int Id { get; set; }
        [Display(Name = "İsim")]
        [Required(ErrorMessage = "İsim alanı boş geçilemez")]
        public string FirstName { get; set; }
        public string? username { get; set; }
        [Required(ErrorMessage = "Soyisim alanı boş geçilemez")]
        [Display(Name = "Soyisim")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email alanı boş geçilemez")]
        [EmailAddress(ErrorMessage = "Email formatı uygun değil")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Şehir alanı boş geçilemez")]
        [Display(Name = "Şehir/İl")]
        public string City { get; set; }
        [Required(ErrorMessage = "İlçe alanı boş geçilemez")]
        [Display(Name = "İlçe")]
        public string Town { get; set; }
        public decimal TotalAmount { get; set; }

    }
}
