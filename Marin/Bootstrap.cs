﻿using System.Reflection;
using Application.Common;
using Application.Core.CommandHandlers;
using Domain.Common;
using Infrastructure.DAL;
using Infrastructure.DAL.EntityFramework;
using Infrastructure.DAL.QueryHandlers;
using Infrastructure.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Marin
{
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

            container.Register<IAuthManager, AuthManager>();
            container.Register<IUserManager, UserManager>();

            //Repository
            container.Register(typeof(IRepository<>), typeof(Repository<>), hybridLifestyle);

            // Register all QueryHandlers
            container.Register(typeof(IQueryHandler<,>), new[] { typeof(FindUserQueryHandler).Assembly });

            //Register all commandhandler
            container.Register(typeof(ICommandHandler<>), new[] { typeof(AddCategoriesCommandHandler).Assembly });
            //Transaction decorator
            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(TransactionScopeDecorator<>));

            //Register simpleinjector event dispatcher
            container.Register<IEventDispatcher, SimpleInjectorEventDispatcher>(Lifestyle.Singleton);

            container.RegisterCollection(typeof(IEventHandler<>), Assembly.GetExecutingAssembly());

            //Email verification with SendGrid
            container.Register<IEmailSender,EmailSender>();

            // Cross-wire ASP.NET services (if any). For instance:
            container.CrossWire<IConfiguration>(app);
            container.CrossWire<ILoggerFactory>(app);
            //Crosswire identity for .net
            container.CrossWire<UserManager<MarinAppUser>>(app);
            container.CrossWire<SignInManager<MarinAppUser>>(app);
            // NOTE: Do prevent cross-wired instances as much as possible.
            // See: https://simpleinjector.org/blog/2016/07/

            //Run latest migration 
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<BudgetDbContext>();
                context.Database.Migrate();
            }
        }
    }

}
