using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBikeAppWebAPI.business.ViewModel.User
{
    public class UpdatePasswordModel
    {
        [Required(ErrorMessage = "Şifre Alanını Boş Geçemezsiniz")]
        public string CurrentPassword { get; set; }
        [Required(ErrorMessage = "Şifre Alanını Boş Geçemezsiniz"), MinLength(7, ErrorMessage = "Şifreniz minimum 8 karakterden oluşmalıdır")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Şifre (Tekrar) Alanını Boş Geçemezsiniz"), MinLength(7, ErrorMessage = "Şifreniz minimum 8 karakterden oluşmalıdır")]
        [Compare("NewPassword", ErrorMessage = "Şifreleriniz Uyuşmuyor")]
        public string NewPasswordAgain { get; set; }
        [Required(ErrorMessage = "Önce Giriş Yapmalısınız")]
        public string UserName { get; set; }
        public string Token { get; set; }
    }
}
