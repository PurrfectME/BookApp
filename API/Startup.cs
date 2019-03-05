using System;
using System.Linq;
using BLL.AppStart;
using BLL.Entities;
using BLL.IdentityWrappers;
using BLL.Interfaces;
using BLL.Services;
using DAL.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<ApplicationContext>(options =>
                {
                    options.UseSqlServer(Configuration["ConnectionStrings:ES"]);
                });

            services.AddIdentity<User, Role>(options =>
                {

                    // Password settings
                    options.Password.RequiredLength = 6;

                    options.Password.RequireDigit = true;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireLowercase = true;

                    // Lockout settings
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5d);
                    options.Lockout.MaxFailedAccessAttempts = 2;
                    options.Lockout.AllowedForNewUsers = true;

                    // SignIn options
                    options.SignIn.RequireConfirmedEmail = false;
                    options.SignIn.RequireConfirmedPhoneNumber = false;

                    // User settings
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();

            services.AddTransient(x => x.GetRequiredService<ApplicationContext>().Users);
            services.AddTransient<CorsSettings>();

            services.AddTransient<IUserManager, UserManagerWrapper>();
            services.AddTransient<ISignInManager, SignInManagerWrapper>();
            services.AddTransient<IRoleManager, RoleManagerWrapper>();

            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<ITokenSettings, TokenSettings>();



            var sp = services.BuildServiceProvider();
            var corsSetting = sp.GetService<IOptions<CorsSettings>>().Value;
            
            services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy = corsSetting.Origins.Any(i => !string.IsNullOrWhiteSpace(i))
                        ? policy.WithOrigins(corsSetting.Origins.Select(i => i.Trim()).ToArray())
                        : policy.AllowAnyOrigin();

                    policy = corsSetting.Headers.Any(i => !string.IsNullOrWhiteSpace(i))
                        ? policy.WithHeaders(corsSetting.Headers.Select(i => i.Trim()).ToArray())
                        : policy.AllowAnyHeader();

                    policy = corsSetting.Methods.Any(i => !string.IsNullOrWhiteSpace(i))
                        ? policy.WithMethods(corsSetting.Methods.Select(i => i.Trim()).ToArray())
                        : policy.AllowAnyMethod();

                    policy.AllowCredentials();
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.Use(async (ctx, next) =>
            {
                ctx.Request.EnableRewind();
                await next();
                if (ctx.Response.StatusCode == 204)
                {
                    ctx.Response.ContentLength = 0;
                }
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            
            app.UseCors("default");

            app.UseMvc(routes => { routes.MapRoute("default", "controller/action/{id}"); });
            app.UseHttpsRedirection();
            
            app.UseAuthentication();
        }
    }
}
