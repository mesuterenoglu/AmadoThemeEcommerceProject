using Amado.BLL.Mapping;
using Amado.BLL.Services;
using Amado.BLL.Services.Interfaces;
using Amado.Core.Entities;
using Amado.Core.Entities.Abstract;
using Amado.DataAccess.Context;
using Amado.DataAccess.Repositories;
using Amado.DataAccess.Repositories.Base;
using Amado.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Amado.BLL.DIContainer
{
    public static class DIContainer
    {
        public static IServiceCollection AddDIContainer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(x =>
                x.UseLazyLoadingProxies().UseSqlite(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("Amado.DataAccess")));

            services.AddIdentity<AppUser, AppRole>(opt =>
            {
                opt.SignIn.RequireConfirmedEmail = false;
                opt.SignIn.RequireConfirmedPhoneNumber = false;

                opt.Password.RequireDigit = true;
                opt.Password.RequiredLength = 8;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = true;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(x =>
            {
                x.LoginPath = new PathString("/User/Login");
                x.LogoutPath = new PathString("/User/Logout");
                x.AccessDeniedPath = new PathString("/User/Denied");
                x.Cookie = new CookieBuilder
                {
                    Name = "AmadoAuthCookie", 
                    HttpOnly = false, 
                    SameSite = SameSiteMode.Lax, 
                    SecurePolicy = CookieSecurePolicy.Always 
                };
                x.SlidingExpiration = true;
                x.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            });

            services.AddScoped<IRepository<BaseEntity>, BaseRepository<BaseEntity>>();

            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductImageRepository, ProductImageRepository>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IProductImageService, ProductImageService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();

            services.AddAutoMapper(typeof(MappingProfile));

            return services;
        }
    }
}
