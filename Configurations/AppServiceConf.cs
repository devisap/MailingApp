// using MailingApp.Models.Functions;
// using MailingApp.Models.QueryBuilders;
// using MailingApp.Repositories;
using MailingApp.Services;
using MailingApp.Utilities;

namespace MailingApp.Configurations
{
    public static class AppServiceConf
    {
        public static void AddAppServiceConfig(this IServiceCollection services)
        {
            services.AddScoped<AuthService>();
            services.AddScoped<PermissionUtil>();
            services.AddScoped<FileUploadUtil>();
        }
    }
}