using System;
using System.Data;
using System.Threading.Tasks;
using Domain.Core;
using Infrastructure.Security;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.DAL.EntityFramework.Seeding
{
    public class Seeder
    {
        private readonly BudgetDbContext _dbContext;
        private readonly UserManager<MarinAppUser> _userManager;

        public Seeder(UserManager<MarinAppUser> userManager, BudgetDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task CreateAppUser()
        {
            const string email = "mathiasmarin86@gmail.com";
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                var newUser = new MarinAppUser(email,email);
                var newDomainUser = new User("Mathias", "Marin", email);
                var result = await _userManager.CreateAsync(newUser, "@Badlösen1234");
                if (!result.Succeeded)
                {
                    throw new ConstraintException("Failed to create user");
                }
                _dbContext.Add(newDomainUser);
                _dbContext.SaveSync();

            }



        }
    }
}

