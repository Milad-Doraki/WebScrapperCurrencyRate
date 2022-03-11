using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace WebScrapperCurrencyRate.Application.Common.Behaviours
{
    public class PerformanceBehaviour<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
        where TResponse : notnull
    {
        private readonly ILogger<PerformanceBehaviour<TRequest, TResponse>> _logger;

        public PerformanceBehaviour(ILogger<PerformanceBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            TResponse response = await next();

            stopwatch.Stop();

            if (stopwatch.ElapsedMilliseconds > TimeSpan.FromSeconds(5).TotalMilliseconds)
            {
                // This method has taken a long time, So we log that to check it later.
                _logger.LogWarning($"{request} has taken {stopwatch.ElapsedMilliseconds} to run completely !");
            }

            return response;
        }
    }
}
