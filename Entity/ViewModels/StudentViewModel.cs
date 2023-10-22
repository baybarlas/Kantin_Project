using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KantindenAl.App.Entity.ViewModels
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        [Display(Name = "İsim")]
        [Required(ErrorMessage = "İsim alanı boş geçilemez")]
        public string FirstName { get; set; }
        [Display(Name = "Soyisim")]
        [Required(ErrorMessage = "Soyisim alanı boş geçilemez")]
        public string LastName { get; set; }
        [Display(Name = "Öğrenci No")]
        [Required(ErrorMessage = "Öğrenci no boş geçilemez")]
        public string StdNo { get; set; }  
        public string SchoolName { get; set; }
        [Display(Name = "Sınıf Adı")]
        [Required(ErrorMessage = "Sınıf Adı alanı boş geçilemez")]
        public string ClassName { get; set; }
        public int SchoolId { get; set; }
        public int ParentId { get; set; }

    }
}
