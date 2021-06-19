using Microsoft.AspNetCore.Http;
using ProgrammerBlog.Entities.Concrete;
using ProgrammerBlog.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammerBlog.Entities.Dto
{
    public class UserResetPasswordDto
    {
                      

        [DisplayName("Şifre")]
        [Required(ErrorMessage = "{0} alanı boş bırakılmamalı.")]  // {0} display name(dinamik)
        [MaxLength(50, ErrorMessage = "{0} alanı {1} karakterden uzun olmamalı.")] // {1} display name(max-length)
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden kısa olmamalı")] // {1} display name(min-length)
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        
        [DisplayName("Yeni Şifre")]
        [Required(ErrorMessage = "{0} alanı boş bırakılmamalı.")]  // {0} display name(dinamik)
        [MaxLength(50, ErrorMessage = "{0} alanı {1} karakterden uzun olmamalı.")] // {1} display name(max-length)
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden kısa olmamalı")] // {1} display name(min-length)
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }


        [DisplayName("Yeni Şifre Tekrar")]
        [Required(ErrorMessage = "{0} alanı boş bırakılmamalı.")]  // {0} display name(dinamik)
        [MaxLength(50, ErrorMessage = "{0} alanı {1} karakterden uzun olmamalı.")] // {1} display name(max-length)
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden kısa olmamalı")] // {1} display name(min-length)
        [DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage ="Girmiş olduğunuz şifreler birbiriyle aynı değil.Lütfen tekrar deneyiniz.")]
        public string NewPasswordConfirm { get; set; }
        
        
    }
}
