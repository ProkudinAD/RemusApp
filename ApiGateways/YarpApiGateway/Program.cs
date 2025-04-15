using MMLib.SwaggerForOcelot.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddSwaggerForOcelot(builder.Configuration);

// Добавьте следующую строку для регистрации контроллеров
builder.Services.AddControllers();

var app = builder.Build();

app.UseSwaggerForOcelotUI();

await app.UseOcelot();

app.Run();