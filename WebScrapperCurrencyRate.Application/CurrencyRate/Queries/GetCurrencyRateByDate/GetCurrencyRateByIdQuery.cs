using MediatR;
using System;

namespace WebScrapperCurrencyRate.Application.CurrencyRates.Queries.GetCurrencyRateByDate
{ 
    public class GetCurrencyAverageRateByDateQuery : IRequest<CurrencyAverageRateDto>
    {
        public string Currency { get; set; }
        public DateTime From { get; set; } 
        public DateTime To { get; set; } 
    }
}