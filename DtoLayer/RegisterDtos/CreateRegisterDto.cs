using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.RegisterDtos
{
    public class CreateRegisterDto
    {
        [Required(ErrorMessage = "Kullanıcı adı gereklidir.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Kullanıcı adı 3-20 karakter olmalı.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Şifre gereklidir.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Ad gereklidir.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Ad 3-20 karakter olmalı.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyad gereklidir.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Soyad 3-20 karakter olmalı.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Email gereklidir.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre doğrulama gereklidir.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor.")]
        public string ConfirmPassword { get; set; }
    }
}
