using BaggageTrackerApi.Middlewares;
using BaggageTrackerApi.Models;
using BaggageTrackerApi.Services;
using Microsoft.EntityFrameworkCore;

namespace BaggageTrackerApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();

        ConfigureServices(builder);
        RegisterServices(builder.Services);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();

        app.MapControllers();

        app.UseHttpsRedirection();
        
        app.UseMiddleware<JwtMiddleware>();
        app.UseAuthorization();

        app.Run();
    }

    private static void ConfigureServices(WebApplicationBuilder builder)
    {
        var appSettingsSection = builder.Configuration.GetSection("AppSettings");
        builder.Services.Configure<AppSettings>(appSettingsSection);
        
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services
            .AddDbContext<BaggageTrackerDbContext>
                (o => o.UseNpgsql(connectionString));
    }

    private static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<UserService>();
    }
}