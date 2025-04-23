using System.Text.Json.Serialization;
using Content.Application;
using Content.Application.Common;
using Content.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Content.WebApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
            {
                options.SerializerOptions.PropertyNameCaseInsensitive = true;
                options.SerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
                options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
            });

            // Добавляем DbContext
            services.AddDbContext<ContentDbContext>(
            options =>
            {
                options.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("Content.Migrations"));  // Указание сборки с миграциями
            });
            services.AddAutoMapper(typeof(ContentApplicationModule));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(ContentApplicationModule).Assembly);
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin() // Разрешить любые домены
                        .AllowAnyHeader() // Разрешить любые заголовки
                        .AllowAnyMethod(); // Разрешить любые HTTP-методы
                });
            });

            services.AddControllers();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Content API", Version = "v1" });
            });

            services.AddLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
            });

        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseCors();

            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Content API V1");
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                // Добавляем поддержку OPTIONS-запросов
                endpoints.MapMethods("{*path}", new[] { "OPTIONS" }, (context) =>
                {
                    context.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:4200");
                    context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
                    context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");
                    context.Response.StatusCode = 200;
                    return Task.CompletedTask;
                });
            });
        }
    }
}