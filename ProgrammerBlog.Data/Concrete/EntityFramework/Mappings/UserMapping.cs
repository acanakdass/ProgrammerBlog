using Microsoft.AspNetCore.Identity;
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

            builder.Property(u => u.Image).HasDefaultValue(true);
            builder.Property(u => u.Image).HasMaxLength(250);

            // Primary key
            builder.HasKey(u => u.Id);

            // Indexes for "normalized" username and email, to allow efficient lookups
            builder.HasIndex(u => u.NormalizedUserName).HasDatabaseName("UserNameIndex").IsUnique();
            builder.HasIndex(u => u.NormalizedEmail).HasDatabaseName("EmailIndex");

            // Maps to the AspNetUsers table
            builder.ToTable("AspNetUsers");

            // A concurrency token for use with the optimistic concurrency checking
            builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            builder.Property(u => u.UserName).HasMaxLength(50);
            builder.Property(u => u.NormalizedUserName).HasMaxLength(256);
            builder.Property(u => u.Email).HasMaxLength(100);
            builder.Property(u => u.NormalizedEmail).HasMaxLength(100);

            // The relationships between User and other entity types
            // Note that these relationships are configured with no navigation properties

            // Each User can have many UserClaims
            builder.HasMany<UserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

            // Each User can have many UserLogins
            builder.HasMany<UserLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

            // Each User can have many UserTokens
            builder.HasMany<UserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

            // Each User can have many entries in the UserRole join table
            builder.HasMany<UserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();




            ////Başlangıç kullanıcıları ekleme
            //var adminUser = new User
            //{
            //    Id = 504,
            //    UserName = "adminuser",
            //    NormalizedUserName = "ADMINUSER",
            //    Email = "acanakdas2@gmail.com",
            //    NormalizedEmail = "acanakdas@gmail.com".ToUpper(),
            //    PhoneNumber = "5074970101",
            //    Image = "defaultUser.png",
            //    EmailConfirmed = true,
            //    PhoneNumberConfirmed = true,
            //    SecurityStamp = Guid.NewGuid().ToString()
            //};
            //adminUser.PasswordHash = CreatePasswordHash(adminUser, "acanakdas");
            //var editorUser = new User
            //{
            //    Id = 505,
            //    UserName = "editoruser",
            //    NormalizedUserName = "EDITORUSER",
            //    Email = "acanakdas2@gmail.com",
            //    NormalizedEmail = "acanakdas00@hotmail.com".ToUpper(),
            //    PhoneNumber = "5074970102",
            //    Image = "defaultUser.png",
            //    EmailConfirmed = true,
            //    PhoneNumberConfirmed = true,
            //    SecurityStamp = Guid.NewGuid().ToString()
            //};
            //editorUser.PasswordHash = CreatePasswordHash(editorUser, "acanakdas");

            //builder.HasData(adminUser, editorUser);

            var adminUser = new User
            {
                //Id = 1,
                UserName = "adminuser",
                NormalizedUserName = "ADMINUSER",
                Email = "adminuser@gmail.com",
                NormalizedEmail = "ADMINUSER@GMAIL.COM",
                PhoneNumber = "+905555555555",
                Image = "defaultUser.png",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            adminUser.PasswordHash = CreatePasswordHash(adminUser, "adminuser");
            var editorUser = new User
            {
                //Id = 2,
                UserName = "editoruser",
                NormalizedUserName = "EDITORUSER",
                Email = "editoruser@gmail.com",
                NormalizedEmail = "EDITORUSER@GMAIL.COM",
                PhoneNumber = "+905555555555",
                Image = "defaultUser.png",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            editorUser.PasswordHash = CreatePasswordHash(editorUser, "editoruser");

            //builder.HasData(adminUser, editorUser);



        }
        private string CreatePasswordHash(User user, string password)
        {
            var passwordHasher = new PasswordHasher<User>();
            return passwordHasher.HashPassword(user, password);
        }
        //private string CreatePasswordHash(User user, string passwordToHash)
        //{
        //    var passwordHasher = new PasswordHasher<User>();
        //    var passwordHashed = passwordHasher.HashPassword(user, passwordToHash);
        //    return passwordHashed;
        //}
    }
}
