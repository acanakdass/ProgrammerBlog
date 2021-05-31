using ProgrammerBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammerBlog.Entities.Dto
{
    public class ArticleUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [DisplayName("Başlık")]
        [Required(ErrorMessage = "{0} alanı boş bırakılmamalı")]
        [MaxLength(150, ErrorMessage = "{0} {1} karakterden uzun olmamalı.")] // {1} display name(max-length)
        [MinLength(10, ErrorMessage = "{0} {1} karakterden kısa olmamalı")] // {1} display name(min-length)
        public string Title { get; set; }

        [DisplayName("Makale İçeriği")]
        [Required(ErrorMessage = "{0} alanı boş bırakılmamalı")]
        [MinLength(50, ErrorMessage = "{0} {1} karakterden kısa olmamalı")] // {1} display name(min-length)
        public string Content { get; set; }

        [DisplayName("Resim")]
        [Required(ErrorMessage = "{0} alanı boş bırakılmamalı")]
        [MaxLength(250, ErrorMessage = "{0} {1} karakterden uzun olmamalı.")] // {1} display name(max-length)
        [MinLength(5, ErrorMessage = "{0} {1} karakterden kısa olmamalı")] // {1} display name(min-length)
        public string Thumbnail { get; set; }

        [DisplayName("Tarih")]
        [Required(ErrorMessage = "{0} alanı boş bırakılmamalı")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [DisplayName("Yazar")]
        [Required(ErrorMessage = "{0} alanı boş bırakılmamalı")]
        [MaxLength(150, ErrorMessage = "{0} {1} karakterden uzun olmamalı.")] // {1} display name(max-length)
        [MinLength(5, ErrorMessage = "{0} {1} karakterden kısa olmamalı")] // {1} display name(min-length)
        public string SeoAuthor { get; set; }

        [DisplayName("Açıklama")]
        [Required(ErrorMessage = "{0} alanı boş bırakılmamalı")]
        [MaxLength(250, ErrorMessage = "{0} {1} karakterden uzun olmamalı.")] // {1} display name(max-length)
        [MinLength(0, ErrorMessage = "{0} {1} karakterden kısa olmamalı")] // {1} display name(min-length)
        public string SeoDescription { get; set; }

        [DisplayName("Etiketler")]
        [Required(ErrorMessage = "{0} alanı boş bırakılmamalı")]
        [MaxLength(250, ErrorMessage = "{0} {1} karakterden uzun olmamalı.")] // {1} display name(max-length)
        [MinLength(5, ErrorMessage = "{0} {1} karakterden kısa olmamalı")] // {1} display name(min-length)
        public string SeoTags { get; set; }

        [DisplayName("Kategori")]
        [Required(ErrorMessage = "{0} alanı boş bırakılmamalı")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }
        
        [DisplayName("Aktif mi?")]
        [Required]
        public bool IsActive { get; set; }
        
        [DisplayName("Silinmiş mi?")]
        [Required]
        public bool IsDeleted { get; set; }
    }
}
