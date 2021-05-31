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
    public class CategoryMapping : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id); //primary key
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.Name).HasMaxLength(70);
            builder.Property(c => c.Description).HasMaxLength(250);
            builder.Property(c => c.CreaterName).IsRequired();
            builder.Property(c => c.CreaterName).HasMaxLength(50);
            builder.Property(c => c.ModifierName).IsRequired();
            builder.Property(c => c.ModifierName).HasMaxLength(50);
            builder.Property(c => c.CreatedDate).IsRequired();
            builder.Property(c => c.ModifiedDate).IsRequired();
            builder.Property(c => c.IsActive).IsRequired();
            builder.Property(c => c.IsDeleted).IsRequired();
            builder.Property(c => c.Note).HasMaxLength(500);
            builder.ToTable("Categories");

            builder.HasData(
                new Category
                {
                    Id = 1,
                    Name = "C#",
                    IsActive = true,
                    IsDeleted = false,
                    CreaterName = "InitialCreate",
                    ModifierName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Note = "C# kategorisi",
                    Description = "C# kategorisi",
                },
                new Category
                {
                    Id = 2,
                    Name = "Javascript",
                    IsActive = true,
                    IsDeleted = false,
                    CreaterName = "InitialCreate",
                    ModifierName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Note = "Javascript kategorisi",
                    Description = "Javascript kategorisi",
                },
                new Category
                {
                    Id = 3,
                    Name = "Asp .Net",
                    IsActive = true,
                    IsDeleted = false,
                    CreaterName = "InitialCreate",
                    ModifierName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Note = "Asp .Net",
                    Description = "Asp .Net kategorisi",
                }
            );
        }

    }
}
