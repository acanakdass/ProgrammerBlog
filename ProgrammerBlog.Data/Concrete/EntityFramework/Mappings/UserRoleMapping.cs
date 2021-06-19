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
    public class UserRoleMapping : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            // Primary key
            builder.HasKey(r => new { r.UserId, r.RoleId });

            // Maps to the AspNetUserRoles table
            builder.ToTable("AspNetUserRoles");

            //var userRoleAdmin = new UserRole
            //{
            //    RoleId = 1,
            //    UserId = 1
            //};
            //var userRoleEditor = new UserRole
            //{
            //    RoleId = 2,
            //    UserId = 2
            //};

            //builder.HasData(userRoleAdmin, userRoleEditor);
            //builder.HasData(
            //     new UserRole
            //     {
            //         RoleId = 1,
            //         UserId = 1
            //     },
            //     new UserRole
            //     {
            //         RoleId = 2,
            //         UserId = 2
            //     }
            //    );
        }
    }
}
