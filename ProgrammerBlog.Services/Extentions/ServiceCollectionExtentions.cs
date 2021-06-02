using Microsoft.Extensions.DependencyInjection;
using ProgrammerBlog.Data.Abstract;
using ProgrammerBlog.Data.Concrete;
using ProgrammerBlog.Data.Concrete.EntityFramework.Context;
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
            serviceCollection.AddScoped<IUnitOfWork,UnitOfWork>();
            serviceCollection.AddScoped<ICategoryService,CategoryManager>();
            serviceCollection.AddScoped<IArticleService,ArticleManager>();

            return serviceCollection;
        }
    }
}
