using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebScrapperCurrencyRate.Application.CurrencyRates.Commands.CreateCurrencyRate;

namespace WebScrapperCurrencyRate.Presentation.Worker
{
    public static class GetCurrencyRate
    {
        public static CreateCurrencyRateCommand ParseHtml(string html)
        {
            //' The Html comes back with unicode character codes, other escaped characters, and
            //' wrapped in double quotes, so I'm using this code to clean it up for what I'm doing.
            html = Regex.Unescape(html);
            html = html.Remove(0, 1);
            html = html.Remove(html.Length - 1, 1);

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
             
            var result = htmlDoc.DocumentNode.SelectNodes("/html/body/div/main/div[3]/div[2]/div/div/div[1]/div/table/tbody/tr")
                                ?.FirstOrDefault(c => c.InnerHtml.Contains("USD"))
                                ?.ChildNodes.Where(c => c.NodeType == HtmlNodeType.Element)
                                .ToList();

            if (result != null)
            {
                return new CreateCurrencyRateCommand
                {
                    Currency = result[1].InnerText,
                    Date = DateTime.Now.Date,
                    Time = DateTime.Now.TimeOfDay,
                    Buy = long.Parse(result[2].InnerText.Replace(",", "").Trim()),
                    Sell = long.Parse(result[3].InnerText.Replace(",", "").Trim()),
                };
            }

            return default; 
        }
    }
}
