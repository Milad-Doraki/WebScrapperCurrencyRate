
using HtmlAgilityPack;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebScrapperCurrencyRate.Application.CurrencyRates.Commands.CreateCurrencyRate;

namespace WebScrapperCurrencyRate.Presentation.Server.BackgroundServices.Tasks
{
    public class GetCurrencyRateTask : ScheduledScopedBackgroundService
    {
        private readonly ILogger<GetCurrencyRateTask> _logger; 

        public GetCurrencyRateTask(
            IServiceScopeFactory serviceScopeFactory,  
            ILogger<GetCurrencyRateTask> logger) : base(serviceScopeFactory)
        { 
            _logger = logger;
        }

        protected override string ScheduleString => "*/30 * * * * *";  // every 30 seconds

        public override async Task<Task> ScheduledExecuteInScope(IServiceProvider serviceProvider, CancellationToken stoppingToken)
        {
            _logger.LogInformation("GetCurrencyRateTask executing - {0}", DateTime.Now);

            try
            {
                string serviceURL = "https://fxmarketrate.cbi.ir/"; //"https://mex.co.ir";

                var Browser = new ScrapingBrowser
                {
                    AllowAutoRedirect = true,
                    AllowMetaRedirect = true
                };
                 
                var result = Browser.NavigateToPage(new Uri(serviceURL)).Html
                             .CssSelect("div[id='MainContent_ViewCashChequeRates_divTimely'] div[class='panel-body'] table tbody tr")
                             ?.FirstOrDefault(c => c.InnerHtml.Contains("USD"))
                             ?.ChildNodes.Where(c => c.NodeType == HtmlNodeType.Element)
                             .ToList();

                var createCurrencyRateCommand = new CreateCurrencyRateCommand
                {
                    Currency = "USD",
                    Date = DateTime.Now.Date,
                    Time = DateTime.Now.TimeOfDay,
                    Buy = 233333,
                    Sell = 24444,
                };

                //if (result != null)
                {
                    //var createCurrencyRateCommand = new CreateCurrencyRateCommand
                    //{
                    //    Currency = result[1].InnerText,
                    //    Date = DateTime.Now.Date,
                    //    Time = DateTime.Now.TimeOfDay,
                    //    Buy = long.Parse(result[2].InnerText.Replace(",", "").Trim()),
                    //    Sell = long.Parse(result[3].InnerText.Replace(",", "").Trim()),
                    //};

                    var _mediator = serviceProvider.GetRequiredService<IMediator>();
                    await _mediator.Send(createCurrencyRateCommand);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetCurrencyRateTask:{exception}", ex.Message + " " + ex.InnerException?.Message);
            }

            return Task.CompletedTask;
        }
    }
}
