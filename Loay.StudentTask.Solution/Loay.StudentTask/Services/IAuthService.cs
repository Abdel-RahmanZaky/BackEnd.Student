using Loay.StudentTask.Core.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace Loay.StudentTask.Services
{
    public interface IAuthService
    {
        Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager);
    }
}
