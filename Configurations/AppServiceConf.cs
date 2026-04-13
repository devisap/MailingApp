using MailingApp.Models.QueryBuilders;
using MailingApp.Services;
using MailingApp.Utilities;

namespace MailingApp.Configurations
{
    public static class AppServiceConf
    {
        public static void AddAppServiceConfig(this IServiceCollection services)
        {
            services.AddScoped<AuthService>();
            services.AddScoped<ZoneService>();
            services.AddScoped<AreaService>();

            services.AddScoped<ZoneQb>();
            services.AddScoped<AreaQb>();

            services.AddScoped<PermissionUtil>();
            services.AddScoped<FileUploadUtil>();
        }
    }
}