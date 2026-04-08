
using System.Security.Claims;
using System.Text;
using MailingApp.Dtos.Generals;
using MailingApp.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
namespace MailingApp.Configurations
{
    public static class AuthenticationConfig
    {
        public static void AddAuthenticationConfig(this IServiceCollection services)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY"))),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                x.Events = new JwtBearerEvents
                {
                    OnChallenge = ctx =>
                    {
                        ctx.HandleResponse();
                        ctx.Response.StatusCode = Const.HTTP_CODE_UNAUTHORIZED;
                        ctx.Response.ContentType = "application/json";
                        var res = new ResStatusFailedDto(Const.RESP_FAILED_PERMISSION, Const.RESP_FAILED_MANDATORY_JWT, Const.HTTP_CODE_UNAUTHORIZED);
                        return ctx.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(res));
                    },
                    OnAuthenticationFailed = ctx =>
                    {
                        ctx.Response.StatusCode = Const.HTTP_CODE_UNAUTHORIZED;
                        ctx.Response.ContentType = "application/json";
                        var res = new ResStatusFailedDto(Const.RESP_FAILED_PERMISSION, Const.RESP_FAILED_PERMISSION_JWT_INVALID, Const.HTTP_CODE_UNAUTHORIZED);
                        return ctx.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(res));
                    },
                    OnForbidden = ctx =>
                    {
                        ctx.Response.StatusCode = Const.HTTP_CODE_FORBIDDEN;
                        ctx.Response.ContentType = "application/json";
                        var res = new ResStatusFailedDto(Const.RESP_FAILED_PERMISSION, Const.RESP_FAILED_PERMISSION_NOT_ALLOWED, Const.HTTP_CODE_UNAUTHORIZED);
                        return ctx.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(res));
                    },
                    OnTokenValidated = ctx =>
                    {
                        var claimsIdentity = ctx.Principal.Identity as ClaimsIdentity;
                        var roleCategory = ctx.Principal.FindFirst("role_category")?.Value;

                        claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, roleCategory));

                        return Task.CompletedTask;
                    }
                };
            });
        }
    }
}