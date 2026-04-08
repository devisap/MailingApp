using dotenv.net;
using MailingApp.Configurations;
using MailingApp.Datas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

DotEnv.Load();

var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

var connectionStringRedis = Environment.GetEnvironmentVariable("REDIS_URL");
// builder.Services.AddHangfire(options =>
// {
//     var redis = ConnectionMultiplexer.Connect(connectionStringRedis);
//     options.UseRedisStorage(redis, options: new RedisStorageOptions {Prefix = $"HANG_FIRE"});
//     options.UseConsole();
// });
// builder.Services.AddHangfireServer();
// builder.Services.AddHostedService<BackgroundJobService>();

// Add services to the container.

builder.Services.AddControllers();

// CONFIG VALIDATION CUSTOM
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAppServiceConfig();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCorsConfig();

builder.Services.AddAuthenticationConfig();

builder.Services.AddAuthorizationConfig();

builder.Services.AddSwaggerConfig();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapGet("/", () => Results.Redirect("/swagger"));
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DefaultModelsExpandDepth(-1);
        c.DefaultModelExpandDepth(1);
        c.DisplayRequestDuration();
        c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
    });
}
else
{
    app.MapGet("/", () => ":))");
}

app.UseStaticFiles();

app.UseHttpsRedirection();

// app.UseHangfireDashboard("/hangfire");

if (app.Environment.IsDevelopment())
{
    app.UseCors("PolicyDev");
}
else
{
    app.UseCors("PolicyProd");
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
