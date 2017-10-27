﻿using System;
using System.Globalization;
using System.Linq;
using Application.Common;
using Domain.Common;
using Infrastructure.DAL;
using Infrastructure.DAL.EntityFramework;
using Infrastructure.DAL.EntityFramework.Seeding;
using Infrastructure.DAL.QueryHandlers;
using Infrastructure.Security;
using Marin.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;

namespace Marin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private Container Container { get;} = new Container();

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc();
            services.AddScoped<IAuthManager, AuthManager>();
            services.AddIdentity<MarinAppUser, IdentityRole>().AddEntityFrameworkStores<BudgetDbContext>()
                .AddDefaultTokenProviders();
            services.AddDbContext<BudgetDbContext>(options =>
                options.UseSqlServer(Configuration["ConnectionStrings:MyConnectionString"]));
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredUniqueChars = 6;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromDays(150);
                options.LoginPath = "/Auth/Login"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
                options.LogoutPath = "/Auth/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
                options.AccessDeniedPath = "/Auth/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
                options.SlidingExpiration = true;
            });
            services.AddTransient<Seeder>();
            Container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IControllerActivator>(
                new SimpleInjectorControllerActivator(Container));
            services.AddSingleton<IViewComponentActivator>(
                new SimpleInjectorViewComponentActivator(Container));

            services.EnableSimpleInjectorCrossWiring(Container);
            services.UseSimpleInjectorAspNetRequestScoping(Container);
            Container.RegisterSingleton(() => Configuration);
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Seeder seeder)
        {
           Bootstrap.InitializeContainer(app,Container);

            Container.Verify();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRequestLocalization();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            seeder.CreateAppUser().Wait();

        }

        public static class Bootstrap
        {
            public static void InitializeContainer(IApplicationBuilder app, Container container)
            {
                var hybridLifestyle = Lifestyle.CreateHybrid(new AsyncScopedLifestyle(), new ThreadScopedLifestyle());

                // Add application presentation components:
                container.RegisterMvcControllers(app);
                container.RegisterMvcViewComponents(app);

               
                //DbContext
                container.Register<IDbContext, BudgetDbContext>(hybridLifestyle);

                container.Register<IAuthManager,AuthManager>();
                container.Register<IUserManager,UserManager>();

               //Repository
                container.Register(typeof(IRepository<>),typeof(Repository<>),hybridLifestyle);

                // Register all QueryHandlers
                container.Register(typeof(IQueryHandler<,>), new[] { typeof(FindUserQueryHandler).Assembly });


                // Cross-wire ASP.NET services (if any). For instance:
                container.CrossWire<ILoggerFactory>(app);
                //Crosswire identity for .net
                container.CrossWire<UserManager<MarinAppUser>>(app);
                container.CrossWire<SignInManager<MarinAppUser>>(app);
                // NOTE: Do prevent cross-wired instances as much as possible.
                // See: https://simpleinjector.org/blog/2016/07/

                //Automatic migrations 
                using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetRequiredService<BudgetDbContext>();
                    context.Database.Migrate();
                }
            }
        }
    }
}

