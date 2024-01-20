using Microsoft.EntityFrameworkCore;
using TechStore.interfaces;
using TechStore.Repository;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using TechStore.Models;

namespace TechStore{
    public class Startup
    {
        private IConfigurationRoot _confString;

        public Startup(IHostEnvironment hostEnv) {
            _confString = new ConfigurationBuilder().SetBasePath(hostEnv.ContentRootPath).AddJsonFile("dbsettings.json").Build();
        }

        public void ConfigureServices(IServiceCollection services) {
            services.AddDbContext<AppDBContent>(options => options.UseSqlServer(_confString.GetConnectionString("DefaultConnection")));
            services.AddTransient<IAllProducts, ProductRepo>();
            services.AddTransient<IProductsType, TypeRepo>();
            services.AddTransient<IAllOrders, OrderRepo>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(sp => Cart.GetCart(sp));

            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
        }

        [Obsolete]
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            app.UseEndpoints(endpoints =>{
                 endpoints.MapControllerRoute(
                 name: "default",
                 pattern: "Home/{action}/{type?}");
             });

            using (var scope = app.ApplicationServices.CreateScope()){
                AppDBContent content = scope.ServiceProvider.GetRequiredService<AppDBContent>();
                DBObjects.Initial(content);
            }
        }
    }
}