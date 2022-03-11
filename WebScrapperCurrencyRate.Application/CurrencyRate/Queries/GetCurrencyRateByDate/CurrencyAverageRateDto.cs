using WebScrapperCurrencyRate.Application.Common.Mappings;
using WebScrapperCurrencyRate.Domain.Entities;
using System;

namespace WebScrapperCurrencyRate.Application.CurrencyRates.Queries.GetCurrencyRateByDate
{
    public class CurrencyAverageRateDto
    { 
        public long? AverageBuy { get; set; }

        public long? AverageSell { get; set; }
    }
}
