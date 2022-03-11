using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MediatR;
using Microsoft.Web.WebView2.Core;
using NCrontab;

namespace WebScrapperCurrencyRate.Presentation.Worker
{
    public partial class BrowserForm : Form
    {
        protected string ScheduleString => "*/30 * * * * *";  // every 30 seconds
        private CrontabSchedule _schedule;
        private DateTime _nextRun;
        private IMediator _mediator;
        
        public BrowserForm(IMediator mediator)
        {
            _mediator = mediator;

            InitializeComponent();
            AttachControlEventHandlers(this.webView2Control);
        }

        #region Event Handlers

        private async void BrowserForm_LoadAsync(object sender, EventArgs e)
        {
            Start();

            _schedule = CrontabSchedule.Parse(ScheduleString, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
            do
            {
                var now = DateTime.Now;
                if (now > _nextRun)
                {
                    Reload();
                    _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
                }
                await Task.Delay(1000); //1 second delay
            }
            while (true);
        }

        private async void WebView2Control_NavigationCompletedAsync(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
           if(this.webView2Control?.CoreWebView2?.DocumentTitle == "وبگاه شرکت صرافی ملی ایران" &&
              this.webView2Control?.CoreWebView2?.Source.StartsWith("https://mex.co.ir") == true)
            {
                var html = await webView2Control.CoreWebView2.ExecuteScriptAsync("document.documentElement.outerHTML;");
                var result = GetCurrencyRate.ParseHtml(html);
                if(result != null)
                {
                    await _mediator.Send(result);
                } 
            }
        }

       

        void AttachControlEventHandlers(Microsoft.Web.WebView2.WinForms.WebView2 control)
        { 
            control.NavigationCompleted += WebView2Control_NavigationCompletedAsync;
        }
         
        #endregion

        #region event
        private void Reload()
        {
            webView2Control.Reload();
        }

        public void Start()
        {
            var rawUrl = "https://mex.co.ir";
            Uri uri = null;

            if (Uri.IsWellFormedUriString(rawUrl, UriKind.Absolute))
            {
                uri = new Uri(rawUrl);
            }
            else if (!rawUrl.Contains(" ") && rawUrl.Contains("."))
            {
                // An invalid URI contains a dot and no spaces, try tacking http:// on the front.
                uri = new Uri("http://" + rawUrl);
            }

            webView2Control.Source = uri;
        }


        #endregion

        
    }
}
