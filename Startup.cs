// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.AspNetCore.Http;g
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using TechStore.interfaces;
using TechStore.Repository;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using TechStore.Models;

namespace TechStore{
    public class Startup
    {
        private IConfigurationRoot _confString;

        public Startup(IHostEnvironment hostEnv){
            _confString = new ConfigurationBuilder().SetBasePath(hostEnv.ContentRootPath).AddJsonFile("dbsettings.json").Build();
        }

        public void ConfigureServices(IServiceCollection services) {
            services.AddDbContext<AppDBContent>(options => options.UseSqlServer(_confString.GetConnectionString("DefaultConnection")));
            services.AddTransient<IAllProducts, ProductRepo>();
            services.AddTransient<IProductsType, TypeRepo>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(sp => Cart.GetCart(sp));

            services.AddMvc();//option => option.EnableEndpointRouting = false); закоментилось для подключения app.UseRouting и useEndpoints
            services.AddMemoryCache();
            services.AddSession();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            //app.UseMvcWithDefaultRoute(); закоментилось для подключения app.UseRouting и useEndpoints
            app.UseEndpoints(endpoints =>{
                endpoints.MapControllerRoute( name: "default", pattern: "{controller=Home}/{action=List}/{id?}");
                endpoints.MapControllerRoute( name: "typeFilter", pattern: "Product/{action}/{type?}");
            });
            // app.UseEndpoints(endpoints =>{
            //     endpoints.MapControllerRoute(
            //     name: "default",
            //     pattern: "{controller=Home}/{action=List}/{id?}");
            // });

            using (var scope = app.ApplicationServices.CreateScope()){
                AppDBContent content = scope.ServiceProvider.GetRequiredService<AppDBContent>();
                DBObjects.Initial(content);
            }
        }
    }
}