using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Content.Infrastructure;

namespace Content.Migrations;

public class Program
{
    public static async Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<ContentDbContext>();
        optionsBuilder.UseNpgsql(
            configuration.GetConnectionString("DefaultConnection"),
            x => x.MigrationsAssembly("Content.Migrations"));

        using (var context = new ContentDbContext(optionsBuilder.Options))
        {
            await context.Database.MigrateAsync();
        }
    }
}