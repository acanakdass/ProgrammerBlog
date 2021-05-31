using Microsoft.EntityFrameworkCore;
using ProgrammerBlog.Data.Concrete.EntityFramework.Mappings;
using ProgrammerBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammerBlog.Data.Concrete.EntityFramework.Context
{
    public class ProgrammerBlogContext:DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder
                .UseSqlServer(@"Server=.\SQLEXPRESS01;Database=ProgrammerBlog;Trusted_Connection=True;Connect TimeOut=30;MultipleActiveResultSets=True;"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Mappings işlemlerini context'e uygulama
            modelBuilder.ApplyConfiguration(new ArticleMapping());
            modelBuilder.ApplyConfiguration(new CategoryMapping());
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new RoleMapping());
            modelBuilder.ApplyConfiguration(new CommentMapping());
        }

    }
}
