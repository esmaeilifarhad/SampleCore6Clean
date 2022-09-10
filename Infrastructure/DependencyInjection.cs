using BusinesLayer.IRepository;
using BusinesLayer.IRepository.Identity;
using BusinesLayer.IRepository.MasterData;
using Domains.Domains.IdentityDomains;
using Infrastructure.Context;
using Infrastructure.Repository;
using Infrastructure.Repository.Identity;
using Infrastructure.Repository.MasterData;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIOC(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
        // services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(
            configuration.GetConnectionString("DefaultConnection")));



            #region AddDependencyInjections
            services.AddScoped(typeof(IRepositoryGeneric<>), typeof(RepositoryGeneric<>));

            services.AddScoped<IIdentityServices, IdentityService>();
            services.AddScoped<IMasterDataRepository, MasterDataRepository>();

            #endregion



            return services;
        }
        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            /*
            //---https://stackoverflow.com/questions/40575776/asp-net-core-identity-login-status-lost-after-deploy
            services.AddDataProtection()
            // This helps surviving a restart: a same app will find back its keys. Just ensure to create the folder.
            .PersistKeysToFileSystem(new DirectoryInfo("\\MyFolder\\keys\\"))
            // This helps surviving a site update: each app has its own store, building the site creates a new app
            .SetApplicationName("pushakshik")
            .SetDefaultKeyLifetime(TimeSpan.FromDays(90));
            */
            //---------------------
            services.AddIdentity<UserApplication, RoleApplication>(options =>
            {
                options.Password.RequiredUniqueChars = 0;

                options.User.RequireUniqueEmail = true;

            })
          .AddEntityFrameworkStores<ApplicationDbContext>()
          .AddDefaultTokenProviders();


            //-------------------------------------identity

            IdentityBuilder builder = services.AddIdentityCore<UserApplication>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;

                options.User.RequireUniqueEmail = false;
                options.User.AllowedUserNameCharacters =
                        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._";


            });

            builder = new IdentityBuilder(builder.UserType, typeof(RoleApplication), builder.Services);
            builder.AddEntityFrameworkStores<ApplicationDbContext>();
            builder.AddRoleManager<RoleManager<RoleApplication>>();
            builder.AddSignInManager<SignInManager<UserApplication>>();

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(7);
                options.Cookie.Name = "AppCookieMYS";
                // options.Cookie.ExpireTimeSpan = TimeSpan.FromDays(15);

                //  options.Cookie.SameSite = SameSiteMode.None;
                options.LoginPath = "/Account/Login"; // Set here your login path.
                options.AccessDeniedPath = "/Account/AccessDenied"; // set here your access denied path.
                options.SlidingExpiration = true;
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                //   options.Events.OnValidatePrincipal
            });




            return services;
        }
    }
}
