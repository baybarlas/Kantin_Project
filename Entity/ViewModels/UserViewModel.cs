using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantindenAl.App.Entity.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [Display(Name = "İsim")]
        [Required(ErrorMessage = "İsim alanı boş geçilemez")]
        public string FirstName { get; set; }
        [Display(Name = "İkinci İsim")]
        public string? MiddleName { get; set; }
        [Required(ErrorMessage = "Soyisim alanı boş geçilemez")]
        [Display(Name = "Soyisim")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Telefon alanı boş geçilemez")]
        [Display(Name = "Telefon Numarası")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Kullanıcı adı boş geçilemez")]
        [Display(Name = "Kullanıcı Adı")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Email alanı boş geçilemez")]
        [EmailAddress(ErrorMessage = "Email formatı uygun değil")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public int SchoolId { get; set; }
        public string? StoreName { get; set; }
        public decimal Balance { get; set; }
        public List<StudentViewModel>? Students { get; set; }
    }
}
