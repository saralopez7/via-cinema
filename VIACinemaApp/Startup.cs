using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using VIACinemaApp.Models;
using Microsoft.AspNetCore.Identity;
using VIACinemaApp.Data;

namespace VIACinemaApp
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
            // Add framework services
            services.AddMvc();

            services.AddDbContext<ViaCinemaAppContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("ViaCinemaAppContext")));

            // Add identity, adds cookies based authentication
            // Add scoped classes for things like UserManager; SignInManager, PasswordHashers....
            // Automatically adds the validates user from a cookie to the HttpContext.user
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ViaCinemaAppContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");
                //routes.MapRoute("",
                //     "{controller=AvailableMovies}/{action = GetMovies}/{date}",
                //     new { controller = "AvailableMovies", action = "GetMovies", date = "" }
                // );
            });
        }
    }
}