using Microsoft.Extensions.DependencyInjection;
using ProgrammerBlog.Data.Abstract;
using ProgrammerBlog.Data.Concrete;
using ProgrammerBlog.Data.Concrete.EntityFramework.Context;
using ProgrammerBlog.Entities.Concrete;
using ProgrammerBlog.Services.Abstract;
using ProgrammerBlog.Services.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammerBlog.Services.Extentions
{
    public static class ServiceCollectionExtentions
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<ProgrammerBlogContext>();
            serviceCollection.AddIdentity<User, Role>(options=> {
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;//şifrede büyük harf olmalı
                options.Password.RequiredLength = 5; //şifrede minimum uzunluk
                options.Password.RequireNonAlphanumeric = false;//şifrede özel karakter olmalı


                options.User.AllowedUserNameCharacters = ""; //kulanıcı adında izin verilen karakterler
                options.User.RequireUniqueEmail = true; //her kullanıcının emaili farklı olmalı 

            }).AddEntityFrameworkStores<ProgrammerBlogContext>();


            serviceCollection.AddScoped<IUnitOfWork,UnitOfWork>();
            serviceCollection.AddScoped<ICategoryService,CategoryManager>();
            serviceCollection.AddScoped<IArticleService,ArticleManager>();

            return serviceCollection;
        }
    }
}
