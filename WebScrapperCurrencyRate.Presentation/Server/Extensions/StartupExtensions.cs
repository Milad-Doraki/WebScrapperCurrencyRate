using WebScrapperCurrencyRate.Presentation.Server.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using WebScrapperCurrencyRate.Presentation.Server.BackgroundServices.Tasks;

namespace WebScrapperCurrencyRate.Presentation.Server.Extensions
{
    public static class StartupExtensions
    {
        public static void UseErrorHandling(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
        }

        public static void AddHostedService(this IServiceCollection services)
        { 
            services.AddHostedService<GetCurrencyRateTask>();
        } 
    }
}
