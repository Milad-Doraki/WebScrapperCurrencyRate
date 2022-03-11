using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebScrapperCurrencyRate.Application.Common.Interfaces;
using WebScrapperCurrencyRate.Infrastructure.Persistence;

namespace WebScrapperCurrencyRate.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var useDatabase = configuration.GetValue<string>("UseDatabase");
            if (useDatabase == "MongoDb")
            {
                // Add services to the container.
                services.Configure<MongoDbSettings>(option =>
                {
                    option.ConnectionString = configuration.GetConnectionString("MongoDbConnection");
                    option.DatabaseName = "WebScrapperCurrencyRate";
                });

                //services.AddSingleton<IApplicationDbContext>(provider => provider.GetRequiredService<MongoDbContext>());
                services.AddSingleton<IApplicationDbContext, MongoDbContext>();
            }
            else if (useDatabase == "SQL")
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("SqlConnection"),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

                services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

            }
            else
            {  //InMemory
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("WebScrapperCurrencyRate"));

                services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

            }

            return services;
        }
    }
}

