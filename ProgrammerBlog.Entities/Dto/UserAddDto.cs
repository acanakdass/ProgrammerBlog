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
    public class UserAddDto{


        [DisplayName("Adı")]
        [Required(ErrorMessage = "{0} alanı boş bırakılmamalı.")]  // {0} display name(dinamik)
        [MaxLength(70, ErrorMessage = "{0} alanı {1} karakterden uzun olmamalı.")] // {1} display name(max-length)
        [MinLength(3, ErrorMessage = "{0} alanı {1} karakterden kısa olmamalı")] // {1} display name(min-length)
        public string FirstName { get; set; }

        [DisplayName("Soyadı")]
        [Required(ErrorMessage = "{0} alanı boş bırakılmamalı.")]  // {0} display name(dinamik)
        [MaxLength(70, ErrorMessage = "{0} alanı {1} karakterden uzun olmamalı.")] // {1} display name(max-length)
        [MinLength(3, ErrorMessage = "{0} alanı {1} karakterden kısa olmamalı")] // {1} display name(min-length)
        public string LastName { get; set; }

        [DisplayName("Kullanıcı Adı")]
        [Required(ErrorMessage = "{0} alanı boş bırakılmamalı.")]  // {0} display name(dinamik)
        [MaxLength(70, ErrorMessage = "{0} alanı {1} karakterden uzun olmamalı.")] // {1} display name(max-length)
        [MinLength(3, ErrorMessage = "{0} alanı {1} karakterden kısa olmamalı")] // {1} display name(min-length)
        public string UserName { get; set; }


        [DisplayName("E-Posta Adresi")]
        [Required(ErrorMessage = "{0} alanı boş bırakılmamalı.")]  // {0} display name(dinamik)
        [MaxLength(50, ErrorMessage = "{0} alanı {1} karakterden uzun olmamalı.")] // {1} display name(max-length)
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden kısa olmamalı")] // {1} display name(min-length)
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Şifre")]
        [Required(ErrorMessage = "{0} alanı boş bırakılmamalı.")]  // {0} display name(dinamik)
        [MaxLength(50, ErrorMessage = "{0} alanı {1} karakterden uzun olmamalı.")] // {1} display name(max-length)
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden kısa olmamalı")] // {1} display name(min-length)
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [DisplayName("Telefon Numarası")]
        [Required(ErrorMessage = "{0} alanı boş bırakılmamalı.")]  // {0} display name(dinamik)
        [MaxLength(13, ErrorMessage = "{0} alanı {1} karakterden uzun olmamalı.")] // {1} display name(max-length)
        [MinLength(10, ErrorMessage = "{0} alanı {1} karakterden kısa olmamalı")] // {1} display name(min-length)
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DisplayName("Fotoğraf")]
        [DataType(DataType.Upload)]
        public IFormFile Image { get; set; }
    }
}
