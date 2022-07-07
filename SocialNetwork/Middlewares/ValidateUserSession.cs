using Microsoft.AspNetCore.Http;
using SocialNet.Core.Application.Helpers;
using SocialNet.Core.Application.ViewModels.User;

namespace WebApp.StockApp.Middlewares
{
    public class ValidateUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ValidateUserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool HasUser()
        {
            UserViewModel userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");

            if (userViewModel == null)
            {
                return false;
            }
            return true;
        }

    }
}
