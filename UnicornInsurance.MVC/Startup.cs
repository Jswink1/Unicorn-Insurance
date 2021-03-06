using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UnicornInsurance.MVC.Contracts;
using UnicornInsurance.MVC.Contracts.Helpers;
using UnicornInsurance.MVC.Helpers;
using UnicornInsurance.MVC.Middleware;
using UnicornInsurance.MVC.Models;
using UnicornInsurance.MVC.Services;
using UnicornInsurance.MVC.Services.Base;

namespace UnicornInsurance.MVC
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
            services.AddHttpContextAccessor();

            services.AddApplicationInsightsTelemetry();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Users/Login";
                options.AccessDeniedPath = "/Home/NotAuthorized";
            });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // Configure Stripe Settings
            services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));
            services.Configure<ClientVerifyEmail>(Configuration.GetSection("ClientVerifyEmail"));
            services.Configure<SwaggerProductionUI>(Configuration.GetSection("SwaggerProductionUI"));

            services.AddTransient<IAuthenticationService, AuthenticationService>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            var httpClientUrl = Configuration.GetSection("HttpClientUrl").Value;
            services.AddHttpClient<IClient, Client>(cl => cl.BaseAddress = new Uri(httpClientUrl));

            services.AddScoped<IMobileSuitService, MobileSuitService>();
            services.AddScoped<IWeaponService, WeaponService>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<IOrderService, Services.OrderService>();
            services.AddScoped<IUserItemService, UserItemService>();
            services.AddScoped<IDeploymentService, DeploymentService>();

            services.AddScoped<IHttpContextHelper, HttpContextHelper>();

            services.AddSingleton<ILocalStorageService, LocalStorageService>();
            services.AddSingleton(x => new BlobServiceClient(Configuration.GetValue<string>("BlobConnectionString")));

            services.AddSingleton<IBlobService, BlobService>();

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}

            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();

            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            StripeConfiguration.ApiKey = Configuration.GetSection("Stripe")["SecretKey"];

            app.UseSession();

            app.UseMiddleware<CartCountMiddleware>();            

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
