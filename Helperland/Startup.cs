using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;  
using Helperland.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Helperland.CustomeHandler;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Helperland
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        
       
        public string ConnectionStrings { get; private set; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            

            services.AddControllersWithViews();
            services.AddDbContext<HelperlandContext>(options => options.UseSqlServer(_config.GetConnectionString("ConnectionString")));
            services.AddSession();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie("Cookies",options =>
            {
                //x.Cookie.Name = "UserLoginCookie",
                options.LoginPath = "/Home/AccesManager";
                options.AccessDeniedPath = "/Home/AccessDenied";
              
            });


            //services.AddAuthorization(config =>
            //{
            //    config.AddPolicy("UserPolicy", policyBuilder =>
            //     {
            //         policyBuilder.UseRequireCustomeClaim(ClaimTypes.Email);
            //         policyBuilder.UseRequireCustomeClaim(ClaimTypes.DateOfBirth);
            //     });
            //});
            //services.AddScoped<IAuthorizationHandler, PolicyAuthorizationHandler>();
            //services.AddScoped<IAuthorizationHandler, RolesAuthorizationHandler>();

            //for Authorization role
           

            services.AddScoped<IAuthorizationHandler, RolesAuthorizationHandler>();
            //services.AddSession(options =>
            //{
            //    options.IdleTimeout = TimeSpan.FromMinutes(20); // set the session expired time.
            //    options.Cookie.HttpOnly = true;
            //    options.Cookie.IsEssential = true;
            //});

            // services.AddScoped<IHelperlandHomeRepository, SQLIHelperlandHomeRepository>();  //sql server integration
            //services.AddHttpContextAccessor();
            //services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //services.AddDistributedMemoryCache();
            
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            //services.AddControllers().AddNewtonsoftJson(options =>
            //options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            //);
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
            }
            
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseHttpsRedirection();
            
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");
            });
        }
    }
}
