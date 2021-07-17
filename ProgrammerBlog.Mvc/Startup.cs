using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProgrammerBlog.Data.Concrete.EntityFramework.Context;
using ProgrammerBlog.Entities.Concrete;
using ProgrammerBlog.Mvc.AutoMapper.Profiles;
using ProgrammerBlog.Services.AutoMapper.Profiles;
using ProgrammerBlog.Services.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProgrammerBlog.Mvc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });

            //services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddSession();
            services.AddAutoMapper(typeof(CategoryProfile), typeof(ArticleProfile), typeof(UserProfile), typeof(ViewModelsProfile)); //Derlenme esnas�nda automapper'�n s�n�flar� taramas�n� sa�lar
            
            
            services.LoadMyServices(connectionString:Configuration.GetConnectionString("LocalSqlServer"));  //identity services etc involved
            
            
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Admin/User/Login");
                options.LogoutPath = new PathString("/Admin/User/Logout");

                options.Cookie = new CookieBuilder
                {
                    Name = "ProgrammerBlog",
                    HttpOnly = true,   //g�venlik, cookie'lerin javacript ile eri�ilmesini engeller, http �zerinde ger�ekle�ir
                    SameSite = SameSiteMode.Strict  , //cookie'ler sadece bizim sitemiz �zerinden gelirse �al��s�n
                    SecurePolicy = CookieSecurePolicy.Always
                };
                options.SlidingExpiration = true; //(default)20 dakika sonra tekrar login olmak gerekir,
                                                  // true ise request yap�ld���nda s�re s�f�rlan�r
                options.ExpireTimeSpan = TimeSpan.FromMinutes(45);
                options.AccessDeniedPath = new PathString("/Admin/User/AccessDenied");


            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseSession();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseNToastNotify();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapAreaControllerRoute(
                    name: "Admin",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
                    );
                endpoints.MapDefaultControllerRoute();

                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
