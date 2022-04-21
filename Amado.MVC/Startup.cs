using Amado.BLL.DIContainer;
using Amado.BLL.Services.Interfaces;
using Amado.Core.DTOs;
using Amado.MVC.MappingClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Amado.MVC
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
            services.AddControllersWithViews().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddDIContainer(Configuration);

            services.AddAutoMapper(typeof(MappingProfileClient));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IServiceProvider provider)
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "MyArea",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            CreateAdmin(provider);
        }
        private async void CreateAdmin(IServiceProvider provider)
        {
            var userService = provider.GetRequiredService<IUserService>();
            var roleService = provider.GetRequiredService<IRoleService>();

            var resultRole = await roleService.RoleExistAsync("Admin");

            var user = new AppUserDto
            {
                Id = Guid.NewGuid().ToString(),
                Email = Configuration["UserSettings:Mail"],
                FirstName = "AdminFirstName",
                LastName = "AdminLastName"
            };

            var resultUser = await userService.AnybyEmailAsync(user.Email);

            if (!resultRole && !resultUser)
            {
                await roleService.CreateRoleAsync("Admin");
                await userService.RegisterAsync(user, Configuration["UserSettings:Password"]);
                await roleService.AssignRoleAsync(user.Email, "Admin");
            }
            else
            {
                var result = await roleService.IsInRoleAsync(user.Email, "Admin");
                if (!result)
                {
                    await roleService.AssignRoleAsync(user.Email, "Admin");
                }
            }
        }
    }
}
