using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Predictr.Interfaces;
using Predictr.Models;

namespace Predictr.Services
{
    public class UserProvider: IUserProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserProvider(IHttpContextAccessor contextAccessor, UserManager<ApplicationUser> userManager)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }

        public string GetUserId()
        {
            return _userManager.GetUserId(_contextAccessor.HttpContext.User);
        }
    }
}
