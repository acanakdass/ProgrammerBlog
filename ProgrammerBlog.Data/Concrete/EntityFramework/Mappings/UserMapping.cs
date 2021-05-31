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
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);//primary key
            builder.Property(u => u.Id).ValueGeneratedOnAdd();
            
            builder.Property(u => u.Username).IsRequired();
            builder.Property(u => u.Username).HasMaxLength(100);
            builder.HasIndex(u => u.Username).IsUnique();

            builder.Property(u => u.FirstName).IsRequired();
            builder.Property(u => u.FirstName).HasMaxLength(100);
            
            builder.Property(u => u.LastName).IsRequired();
            builder.Property(u => u.LastName).HasMaxLength(100);
            
            builder.Property(u => u.Email).IsRequired();
            builder.Property(u => u.Email).HasMaxLength(150);
            builder.HasIndex(u => u.Email).IsUnique();

            builder.Property(u => u.PasswordHash).IsRequired();
            builder.Property(u => u.PasswordHash).HasColumnType("VARBINARY(500)");
            builder.Property(u => u.PasswordHash).HasColumnType("VARBINARY(500)");

            builder.Property(u => u.Image).IsRequired();

            builder.HasOne<Role>(u => u.Role).WithMany(r => r.Users).HasForeignKey(u => u.RoleId);

            builder.Property(c => c.CreaterName).IsRequired();
            builder.Property(c => c.CreaterName).HasMaxLength(50);
            builder.Property(c => c.ModifierName).IsRequired();
            builder.Property(c => c.ModifierName).HasMaxLength(50);
            builder.Property(c => c.CreatedDate).IsRequired();
            builder.Property(c => c.ModifiedDate).IsRequired();
            builder.Property(c => c.IsActive).IsRequired();
            builder.Property(c => c.IsDeleted).IsRequired();
            builder.Property(c => c.Note).HasMaxLength(500);

            builder.ToTable("Users");

            builder.HasData(new User
            {
                Id = 1,
                RoleId = 1,
                Username="acanakdas",
                FirstName = "Ahmet Can",
                LastName = "Akdaş",
                Email = "acanakdas@gmail.com",
                IsActive = true,
                IsDeleted = false,
                CreaterName = "InitialCreate",
                ModifierName = "InitialCreate",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                Note = "Admin kullanıcısı",
                Description = "Admin kullanıcısı",
                PasswordHash=Encoding.ASCII.GetBytes("79ea8594e4342d6061df2696bf98f78c"),//acanakdas
                Image= "https://png.pngtree.com/png-vector/20190710/ourmid/pngtree-user-vector-avatar-png-image_1541962.jpg"
            });
        }
    }
}
