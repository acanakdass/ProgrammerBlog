using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgrammerBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammerBlog.Data.Concrete.EntityFramework.Mappings
{
    public class ArticleMapping : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(a => a.Id);//primary key
            builder.Property(a => a.Id).ValueGeneratedOnAdd();//veri eklendiğinde oluştur
            builder.Property(a => a.Title).HasMaxLength(100);//maks 100 karakter
            builder.Property(a => a.Title).IsRequired(true);
            builder.Property(a => a.Content).IsRequired();//default true
            builder.Property(a => a.Content).HasColumnType("NVARCHAR(MAX)");//MAKS DEĞER ALSIN
            builder.Property(a => a.Date).IsRequired();//DEFAULT TRUE
            builder.Property(a => a.SeoAuthor).IsRequired();//DEFAULT TRUE
            builder.Property(a => a.SeoAuthor).HasMaxLength(50);
            builder.Property(a => a.SeoDescription).HasMaxLength(150);
            builder.Property(a => a.SeoDescription).IsRequired();//DEFAULT TRUE
            builder.Property(a => a.SeoTags).IsRequired();
            builder.Property(a => a.SeoTags).HasMaxLength(75);
            builder.Property(a => a.ViewsCount).IsRequired();
            builder.Property(a => a.CommentCount).IsRequired();
            builder.Property(a => a.Thumbnail).IsRequired();
            builder.Property(a => a.Thumbnail).HasMaxLength(250);
            builder.Property(a => a.CreaterName).IsRequired();
            builder.Property(a => a.CreaterName).HasMaxLength(50);
            builder.Property(a => a.ModifierName).IsRequired();
            builder.Property(a => a.ModifierName).HasMaxLength(50);
            builder.Property(a => a.CreatedDate).IsRequired();
            builder.Property(a => a.ModifiedDate).IsRequired();
            builder.Property(a => a.IsActive).IsRequired();
            builder.Property(a => a.IsDeleted).IsRequired();
            builder.Property(a => a.Note).HasMaxLength(500);

            //bire çok ilişki category/article
            builder.HasOne<Category>(a => a.Category).WithMany(c => c.Articles).HasForeignKey(a => a.CategoryId);
            //çpka çok ilişki article/user
            builder.HasOne<User>(a => a.User).WithMany(u => u.Articles).HasForeignKey(a => a.UserId);

            builder.ToTable("Articles");

            //builder.HasData(
            //    new Article
            //    {
            //        Id = 1,
            //        CategoryId = 1,
            //        Title = ".Net Core'da file upload ile formdan veritabanına resim kaydetme",
            //        Content = "Lorem Ipsum is simply dummy text of the printing and typesetting industry." +
            //    " Lorem Ipsum has been the industry's standard dummy text ever since the 1500s," +
            //    " when an unknown printer took a galley of type and scrambled it to make a type specimen book." +
            //    " It has survived not only five centuries, but also the leap into electronic typesetting, " +
            //    "remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset " +
            //    "sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like " +
            //    "Aldus PageMaker including versions of Lorem Ipsum.",
            //        Thumbnail = "default.jpg",
            //        SeoDescription = ".net core, file upload",
            //        SeoTags = "C#,.Net,Asp.Net, Image Upload",
            //        SeoAuthor = "Ahmet Can Akdaş",
            //        IsActive = true,
            //        IsDeleted = false,
            //        CreaterName = "InitialCreate",
            //        ModifierName = "InitialCreate",
            //        CreatedDate = DateTime.Now,
            //        ModifiedDate = DateTime.Now,
            //        Note = "Admin kullanıcısı",
            //        UserId = 1
            //    },
            //    new Article
            //    {
            //        Id = 2,
            //        CategoryId = 2,
            //        Title = ".Net Core'da authentication",
            //        Content = "There are many variations of passages of Lorem Ipsum available, but the majority have" +
            //        " suffered alteration in some form, by injected humour, or randomised words which don't look even " +
            //        "slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there" +
            //        " isn't anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the" +
            //        " Internet tend to repeat predefined chunks as necessary, making this the first true generator on " +
            //        "the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence " +
            //        "structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is" +
            //        " therefore always free from repetition, injected humour, or non-characteristic words etc.",
            //        Thumbnail = "default.jpg",
            //        SeoDescription = ".net core, authentication",
            //        SeoTags = "C#,.Net,Asp.Net, authentication",
            //        SeoAuthor = "Ahmet Can Akdaş",
            //        IsActive = true,
            //        IsDeleted = false,
            //        CreaterName = "InitialCreate",
            //        ModifierName = "InitialCreate",
            //        CreatedDate = DateTime.Now,
            //        ModifiedDate = DateTime.Now,
            //        Note = "Admin kullanıcısı",
            //        UserId = 1
            //    }
            //);
        }
    }
}
