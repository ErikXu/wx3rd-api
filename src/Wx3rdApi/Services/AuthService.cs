using Microsoft.Extensions.Caching.Memory;
using Wx3rdApi.Models.Wx3rd;

namespace Wx3rdApi.Services
{
    public interface IAuthService
    {
        LoginInfo GetLoginInfo(HttpRequest request);
    }

    public class AuthService : IAuthService
    {
        private readonly IMemoryCache _cache;

        public AuthService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public LoginInfo GetLoginInfo(HttpRequest request)
        {
            if (request.Cookies.TryGetValue("uuid", out var uuid))
            {
                var loginInfo = _cache.Get<LoginInfo>(uuid);
                return loginInfo;
            }

            return null;
        }
    }
}
