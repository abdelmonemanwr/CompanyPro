using CompanyPro.AutoMapper;
using CompanyPro.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyPro
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ITIContext>(options =>
            {
                options
                .UseLazyLoadingProxies()
                .UseSqlServer(builder.Configuration.GetConnectionString("iti"));
            });


            // usermanager, userstore, signmanager, 
            builder.Services
                .AddIdentity<ApplicationUser, IdentityRole<int>>()
                .AddEntityFrameworkStores<ITIContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSession(sessionOptions => {
                sessionOptions.IdleTimeout = TimeSpan.FromMinutes(60); // session abort after 60 min
            });

            var licenseKey = File.ReadAllText(Path.Combine("AutoMapper", "AutoMapperLicense.txt")).Trim();

            builder.Services.AddAutoMapper(cfg=> {
                cfg.AddProfile<MappingProfile>();
                cfg.LicenseKey = licenseKey;

            });

            var app = builder.Build();

            #region Custom Middlewares

            //app.Use(async (httpContext, next) => {
            //    //httpContext.Request.Cookies
            //    //httpContext.Request.Path.Value.EndsWith("html")
            //    await httpContext.Response.WriteAsync("1st middleware\n");
            //    await next.Invoke();
            //    await httpContext.Response.WriteAsync("11st middleware\n");
            //});

            //app.Use(async (httpContext, next) => {
            //    await httpContext.Response.WriteAsync("2nd middleware\n");
            //    await next.Invoke();
            //    await httpContext.Response.WriteAsync("22nd middleware\n");
            //});

            //app.Run(async (httpContext) => {
            //    await httpContext.Response.WriteAsync("3rd middleware\n");
            //});

            #endregion

            #region Middleware Ordering 

            /*
                 Exception Handling
                 Http Redirection
                 Static File
                 Routing
                 CQRS
                 Authentication (who are you)     >> username & password
                 Authorization  (what can you do) >> Role
                 Custom Middleware
                 Mapping
                 Run
             */


            #endregion


            #region Built-in Middlewares
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
            #endregion

            app.Run(); // not a middleware
        }
    }
}
