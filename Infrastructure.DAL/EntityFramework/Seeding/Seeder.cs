using System;
using System.Data;
using System.Threading.Tasks;
using Infrastructure.Security;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.DAL.EntityFramework.Seeding
{
    public class Seeder
    {
        private readonly UserManager<MarinAppUser> _userManager;

        public Seeder(UserManager<MarinAppUser> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task CreateAppUser()
        {
            const string email = "mathiasmarin86@gmail.com";
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                var newUser = new MarinAppUser(email,email);
                var result = await _userManager.CreateAsync(newUser, "@Badlösen1234");
                if (!result.Succeeded)
                {
                    throw new ConstraintException("Failed to create user");
                }
                
            }



        }
    }
}
