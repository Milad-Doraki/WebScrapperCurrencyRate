using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;
using WebScrapperCurrencyRate.Application;
using WebScrapperCurrencyRate.Infrastructure;
using System;
using System.Windows.Forms;

namespace WebScrapperCurrencyRate.Presentation.Worker
{
    static class Program
    { 
        [STAThread]
        static void Main(string[] args)
        {
            System.Windows.Forms.Application.SetHighDpiMode(HighDpiMode.SystemAware);
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false); 

            var host = CreateHostBuilder(args).Build(); 
            var services = host.Services;
            var mainForm = services.GetRequiredService<BrowserForm>();
            System.Windows.Forms.Application.Run(mainForm);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
                 Host.CreateDefaultBuilder(args) 
                .ConfigureLogging((context, logging) =>
                {
                    logging.AddConfiguration(context.Configuration.GetSection("Logging"));
                    logging.AddEventLog(new EventLogSettings()
                    {
                        SourceName = "WebScrapperCurrencyRate.Presentation.WorkerSource",
                        LogName = "WebScrapperCurrencyRate.Presentation.WorkerLog"
                    });
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddApplication();
                    services.AddInfrastructure(hostContext.Configuration);

                    services.AddSingleton<BrowserForm>(); 
                }); 
    }
}
