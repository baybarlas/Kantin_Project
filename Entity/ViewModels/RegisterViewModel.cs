using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KantindenAl.App.Entity.ViewModels
{
	public class RegisterViewModel
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
		[Required(ErrorMessage = "Kullanıcı adı boş geçilemez")]
		[Display(Name = "Kullanıcı Adı")]
		public string Username { get; set; }
		[Required(ErrorMessage = "Telefon alanı boş geçilemez")]
		[Display(Name = "Telefon Numarası")]
		public string PhoneNumber { get; set; }
		[Required(ErrorMessage = "Email alanı boş geçilemez")]
		[EmailAddress(ErrorMessage = "Email formatı uygun değil")]
		[Display(Name = "Email")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Şifre boş geçilemez")]
		[Display(Name = "Şifre")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required(ErrorMessage = "Tekrar şifresi boş geçilemez")]
		[Compare("Password", ErrorMessage = "Şifreler uyuşmuyor!")]
		[Display(Name = "Şifre Tekrar")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }
	}
}
