using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApplication1.Repositories;
using WebApplication1.Data;

namespace WebApplication1
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IRepository, MyRepository>();
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ZooContext>(options => options.UseSqlServer(connectionString).EnableSensitiveDataLogging());
            services.AddControllersWithViews();
           
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ZooContext zooContext)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            zooContext.Database.EnsureDeleted();
            zooContext.Database.EnsureCreated();

            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
