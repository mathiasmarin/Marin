using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Security
{
    public interface IUserManager
    {
        Task<IdentityResult> CreateUser(string firstName, string lastName, string email, string password);
    }
}