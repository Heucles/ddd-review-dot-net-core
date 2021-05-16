using DddInPractice.Logic.Utils;
using DddInPractice.UI.Models;
using DddInPractice.UI.Models.Impl;
using DddInPractice.UI.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NHibernate;

namespace DddInPractice.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Initer.Init(@"Server=localhost;Database=DddInPractice;User Id=sa;Password=reviewddd@123;");
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddControllers(options => options.Filters.Add(new HttpResponseExceptionFilter()));

            //the row bellow making sure there is only one instance of the SnackMachine through all the application life cycle
            services.AddSingleton<ISnackMachineContainer, SnackMachineContainer>();

            //services.AddScoped<ISnackMachineContainer,SnackMachineContainer>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=SnackMachine}/{action=Index}/{id?}");
            });
        }
    }
}
