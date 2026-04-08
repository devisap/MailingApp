namespace MailingApp.Configurations
{
    public static class AppCorsConf
    {
        public static void AddCorsConfig(this IServiceCollection services)
        {
            services.AddCors(x =>
            {
                x.AddPolicy("PolicyDev", policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });

                x.AddPolicy("PolicyProd", policy =>
                {
                    policy.WithOrigins(Environment.GetEnvironmentVariable("BASE_URL"), Environment.GetEnvironmentVariable("CLIENT_URL"))
                        .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
                        .WithHeaders("Content-Type", "Authorization");

                });
            });
        }
    }
}