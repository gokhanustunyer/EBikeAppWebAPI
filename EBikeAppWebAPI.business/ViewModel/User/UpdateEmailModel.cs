using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBikeAppWebAPI.business.ViewModel.User
{
    public class UpdateEmailModel
    {
        public string? UserName { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Yeni E-Posta Alanını Boş Geçemezsiniz"), MinLength(7, ErrorMessage = "Geçersiz Uzunlukta E-Posta Adresi")]
        public string NewEmail { get; set; }
        [Required(ErrorMessage = "Yeni E-Posta (Tekrar) Alanını Boş Geçemezsiniz"), MinLength(7, ErrorMessage = "Geçersiz Uzunlukta E-Posta Adresi")]
        [Compare("NewEmail", ErrorMessage = "E-Postalar Uyuşmuyor")]
        public string NewEmailAgain { get; set; }
        [Required(ErrorMessage = "Şifre Alanını Boş Geçemezsiniz")]
        public string Password { get; set; }
    }
}
