using JotFinalProject.Data;
using JotFinalProject.Models;
using JotFinalProject.Models.Interfaces;
using JotFinalProject.Models.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JotFinalProject
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder().AddEnvironmentVariables();
            builder.AddUserSecrets<Startup>();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            CognitiveService.ApiKey = Configuration["Api:Key"];
            services.AddMvc();

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireLowercase = true;
            })
                .AddEntityFrameworkStores<ApplicationDBContext>()
                .AddDefaultTokenProviders();

            services.AddDbContext<JotDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ProductionDb")));

            services.AddDbContext<ApplicationDBContext>(options =>

            options.UseSqlServer(Configuration.GetConnectionString("UsersDb")));

            
            services.AddTransient<IImageUploaded, ImageUploadedService>();
            services.AddTransient<ICognitive, CognitiveService>();
            services.AddTransient<INote, NoteService>();
            services.AddTransient<ICategory, CategoryService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=Login}/");
            });
        }
    }
}
