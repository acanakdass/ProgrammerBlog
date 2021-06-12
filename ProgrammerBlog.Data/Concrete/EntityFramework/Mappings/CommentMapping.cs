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
    public class CommentMapping : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c => c.Id);//primary key
            builder.Property(c => c.Id).ValueGeneratedOnAdd();//primary key
            builder.Property(c => c.Text).IsRequired();
            builder.Property(c => c.Text).HasMaxLength(1000);
            builder.Property(c => c.CreaterName).IsRequired();
            builder.Property(c => c.CreaterName).HasMaxLength(50);
            builder.Property(c => c.ModifierName).IsRequired();
            builder.Property(c => c.ModifierName).HasMaxLength(50);
            builder.Property(c => c.CreatedDate).IsRequired();
            builder.Property(c => c.ModifiedDate).IsRequired();
            builder.Property(c => c.IsActive).IsRequired();
            builder.Property(c => c.IsDeleted).IsRequired();
            builder.Property(c => c.Note).HasMaxLength(500);

            builder.HasOne(c => c.Article).WithMany(a => a.Comments).HasForeignKey(a => a.ArticleId);
            builder.ToTable("Comments");

            //builder.HasData(
            //    new Comment
            //    {
            //        Id = 1,
            //        ArticleId = 1,
            //        Text = "It is a long established fact that a reader will be distracted by the readable" +
            //        " content of a page when looking at its layout. The point of using Lorem Ipsum is" +
            //        " that it has a more-or-less normal distribution of letters, as opposed to using 'Content " +
            //        "here, content here', making it look like readable English. Many desktop publishing packages " +
            //        "and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' " +
            //        "will uncover many web sites still in their infancy. Various versions have evolved over the years, " +
            //        "sometimes by accident, sometimes on purpose (injected humour and the like).",
            //        IsActive = true,
            //        IsDeleted = false,
            //        CreaterName = "InitialCreate",
            //        ModifierName = "InitialCreate",
            //        CreatedDate = DateTime.Now,
            //        ModifiedDate = DateTime.Now,
            //        Note = "Ex Comment",
            //    },
            //     new Comment
            //     {
            //         Id = 2,
            //         ArticleId = 2,
            //         Text = "It is a long established fact that a reader will be distracted by the readable" +
            //        " content of a page when looking at its layout. The point of using Lorem Ipsum is" +
            //        " that it has a more-or-less normal distribution of letters, as opposed to using 'Content " +
            //        "here, content here', making it look like readable English. Many desktop publishing packages " +
            //        "and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' " +
            //        "will uncover many web sites still in their infancy. Various versions have evolved over the years, " +
            //        "sometimes by accident, sometimes on purpose (injected humour and the like).",
            //         IsActive = true,
            //         IsDeleted = false,
            //         CreaterName = "InitialCreate",
            //         ModifierName = "InitialCreate",
            //         CreatedDate = DateTime.Now,
            //         ModifiedDate = DateTime.Now,
            //         Note = "Ex Comment 2",
            //     }
            //    );
        }
    }
}
