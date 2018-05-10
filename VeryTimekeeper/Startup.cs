using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using VeryTimekeeper.Models;

//Add this
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace VeryTimekeeper
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

        //    services.AddEntityFrameworkMySql()
        //      .AddDbContext<ApplicationDbContext>(options =>
        //          options.UseMySql(Configuration["ConnectionStrings:DefaultConnection"])
        //      );

        //    services.AddIdentity<ApplicationUser, IdentityRole>()
        //      .AddEntityFrameworkStores<ApplicationDbContext>()
        //      .AddDefaultTokenProviders();


            
        //    services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        //    {
        //        options.Password.RequireUppercase = false;
        //        options.Password.RequireNonAlphanumeric = false;
        //        options.Password.RequiredLength = 0;
        //        options.Password.RequireDigit = false;
        //    });
        }

        public void Configure(IApplicationBuilder app)
        {
           
            app.UseIdentity();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}