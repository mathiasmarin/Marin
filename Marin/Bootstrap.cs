using System.Linq;
using System.Reflection;
using System.Security.Claims;
using Application.Common;
using Application.Core.CommandHandlers;
using Application.Core.Decorators;
using Domain.Common;
using Domain.Core;
using Infrastructure.DAL;
using Infrastructure.DAL.EntityFramework;
using Infrastructure.DAL.QueryHandlers;
using Infrastructure.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
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

            container.RegisterDecorator(typeof(IQueryHandler<,>), typeof(CacheDecorator<,>));

            //Register all commandhandler
            container.Register(typeof(ICommandHandler<>), new[] { typeof(AddCategoriesCommandHandler).Assembly });
            //Transaction decorator
            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(TransactionScopeDecorator<>));
            //Cache remover
            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(CacheRemover<>));

            //Register simpleinjector event dispatcher
            container.Register<IEventDispatcher, SimpleInjectorEventDispatcher>(Lifestyle.Singleton);

            container.Register(typeof(IEventHandler<>), Assembly.GetExecutingAssembly());

            //Email verification with SendGrid
            container.Register<IEmailSender, EmailSender>();

            // Cross-wire ASP.NET services (if any). For instance:
            container.CrossWire<IConfiguration>(app);
            container.CrossWire<ILoggerFactory>(app);
            //Crosswire identity for .net
            container.CrossWire<UserManager<MarinAppUser>>(app);
            container.CrossWire<SignInManager<MarinAppUser>>(app);
            container.CrossWire<IMemoryCache>(app);
            // NOTE: Do prevent cross-wired instances as much as possible.
            // See: https://simpleinjector.org/blog/2016/07/

            //Run latest migration 
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<BudgetDbContext>();
                var securityDbContext = serviceScope.ServiceProvider.GetRequiredService<SecurityDbContext>();
                context.Database.Migrate();
                securityDbContext.Database.Migrate();

                if (!securityDbContext.Users.Any(x => x.Email.Equals("janneb@mailinator.com")))
                {
                    var um = serviceScope.ServiceProvider.GetRequiredService<UserManager<MarinAppUser>>();
                    var newUser = new User("Jan", "Banan", "janneb@mailinator.com");
                    var appUser = new MarinAppUser(newUser) { EmailConfirmed = true };
                    context.GetSet<User>().Add(newUser);
                    context.SaveChanges();

                    um.CreateAsync(appUser, "Test1234").Wait();
                    um.AddClaimAsync(appUser, new Claim("UserId", newUser.Id.ToString())).Wait();
                    um.AddClaimAsync(appUser, new Claim("Name", newUser.GetFullName())).Wait();
                    um.UpdateAsync(appUser).Wait();

                    securityDbContext.SaveChanges();


                }


            }
        }
    }

}

