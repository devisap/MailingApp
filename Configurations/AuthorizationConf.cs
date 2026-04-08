using System.Security.Claims;
using MailingApp.Utilities;

namespace MailingApp.Configurations
{
    public static class AuthorizationConf
    {
        public static void AddAuthorizationConfig (this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Const.POLICY_ROLE_ADMIN, policy =>
                    policy.RequireClaim(ClaimTypes.Role, Const.ROLE_ADMIN));

                options.AddPolicy(Const.POLICY_ROLE_USER, policy =>
                    policy.RequireClaim(ClaimTypes.Role, Const.ROLE_USER));
            });
        }
    }
}