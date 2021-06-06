using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammerBlog.Entities.Dto
{
    public class CategoryAddDto
    {
        [Required]
        public int Id { get; set; }


        [DisplayName("Kategori Adı")]
        [Required(ErrorMessage = "{0} alanı boş bırakılmamalı.")]  // {0} display name(dinamik)
        [MaxLength(70,ErrorMessage ="{0} {1} karakterden uzun olmamalı.")] // {1} display name(max-length)
        [MinLength(3,ErrorMessage = "{0} {1} karakterden kısa olmamalı")] // {1} display name(min-length)
        public string Name { get; set; }

        [DisplayName("Kategori Açıklaması")]
        [MaxLength(70, ErrorMessage = "{0} {1} karakterden uzun olmamalı.")] // {1} display name(max-length)
        [MinLength(3, ErrorMessage = "{0} {1} karakterden kısa olmamalı.")] // {1} display name(min-length)

        public string Description { get; set; }

        [DisplayName("Kategori Hakkında Not")]
        [MaxLength(70, ErrorMessage = "{0} {1} karakterden uzun olmamalı.")] // {1} display name(max-length)
        [MinLength(3, ErrorMessage = "{0} {1} karakterden kısa olmamalı.")] // {1} display name(min-length)
        public string Note { get; set; }

        [DisplayName("Aktif mi?")]
        [Required(ErrorMessage = "{0} alanı boş bırakılmamalı.")]  // {0} display name(dinamik)
        public bool IsActive { get; set; }
        [DisplayName("Silindi mi?")]
        [Required(ErrorMessage = "{0} alanı boş bırakılmamalı.")]  // {0} display name(dinamik)
        public bool IsDeleted { get; set; }
    }
}
