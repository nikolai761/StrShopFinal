using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StrShop.Data;
using StrShop.Data.Interfaces;
using StrShop.Data.Repository;
using StrShop.Data.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;


namespace StrShop
{
    public class Startup
    {

        private IConfigurationRoot _confstring;

        public Startup(Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment)
        {
            _confstring = new ConfigurationBuilder().SetBasePath(hostingEnvironment.ContentRootPath).AddJsonFile("dbsettings.json").Build();
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DBconnection>(options => options.UseSqlServer(_confstring.GetConnectionString("DefaultConnection")));



            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/SignIn");
                options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/SignIn");
            });

            services.AddTransient<IUsers, UserRepository>();
            services.AddTransient<IAllItems, ItemRepository>();
            services.AddTransient<IItemCategory, CategoryRepository>();
            services.AddTransient<IProducer, ProducerRepository>();
            services.AddTransient<IStorages, StoragesRepository>();
            services.AddTransient<IAllOrders, OrdersRepository>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(sp => ShopCart.GetCart(sp));

            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();



            app.UseStatusCodePages();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseMvcWithDefaultRoute();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(name: "categoryFilter", "item/{action}/{category?}", defaults: new { Controller = "Items", action = "ItemList" });
            });

            using (var scope = app.ApplicationServices.CreateScope())
            {
                DBconnection content = scope.ServiceProvider.GetRequiredService<DBconnection>();
                DbObject.initial(content);
            }


        }
    }
}
