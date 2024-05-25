using System.Text.Json.Serialization;
using BaggageTrackerApi.Middlewares;
using BaggageTrackerApi.Models;
using BaggageTrackerApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using QRCoder;

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
        builder.Services.AddSwaggerGen(swagger =>
        {
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Baggage Tracker API",
                Description = "RESTful API for Baggage Tracking application."
            });
            
            swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                In = ParameterLocation.Header,
                Description = "JWT authorization header using the Bearer scheme. e.g. Bearer [space] JWT_TOKEN_HERE"
            });
            swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        
        builder.Services.AddControllers()
            .AddJsonOptions(options => 
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        ConfigureServices(builder);

        var corsPolicyName = GetCorsSettings(builder).GetSection("DefaultPolicyName").Get<string>();
        ConfigureCors(builder, corsPolicyName!);
        RegisterServices(builder.Services);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();

        app.UseCors(corsPolicyName!);

        app.MapControllers();

        app.UseHttpsRedirection();
        
        app.UseMiddleware<JwtMiddleware>();
        app.UseAuthorization();

        app.Run();
    }

    private static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.Configure<AppSettings>(GetAppSettings(builder));
        
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services
            .AddDbContext<BaggageTrackerDbContext>
                (o => o.UseNpgsql(connectionString));
        
        builder.Services
            .AddAutoMapper(cfg => 
                cfg.AddProfile(new AutoMapperProfile()));
    }

    private static void ConfigureCors(WebApplicationBuilder builder, string policyName)
    {
        var allowedOrigins = GetCorsSettings(builder).GetSection("AllowedOrigins").Get<string[]>();
        
        builder.Services
            .AddCors(option => 
                option.AddPolicy(name: policyName, policyBuilder => policyBuilder
                        .WithOrigins(allowedOrigins!)
                        .AllowAnyHeader()
                        .WithExposedHeaders("Content-Disposition")
                        .AllowAnyMethod()));

    }

    private static IConfigurationSection GetAppSettings(WebApplicationBuilder builder)
    {
        return builder.Configuration.GetSection("AppSettings");
    }
    
    private static IConfigurationSection GetCorsSettings(WebApplicationBuilder builder)
    {
        return builder.Configuration.GetSection("AppSettings:Cors");
    }

    private static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<UserService>();
        services.AddScoped<AuthenticationService>();
        services.AddScoped<BaggageTrackingService>();
        services.AddScoped<QrCodeGenerationService>();
        services.AddScoped<QRCodeGenerator>();
        services.AddScoped<UbcProcessor>();
        services.AddScoped<HelpService>();
        services.AddSingleton<PasswordGenerator>();
    }
}