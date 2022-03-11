using WebScrapperCurrencyRate.Presentation.Server.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection; 

namespace WebScrapperCurrencyRate.Presentation.Server.Extensions
{
    public static class StartupExtensions
    {
        public static void UseErrorHandling(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
        } 
    }
}
