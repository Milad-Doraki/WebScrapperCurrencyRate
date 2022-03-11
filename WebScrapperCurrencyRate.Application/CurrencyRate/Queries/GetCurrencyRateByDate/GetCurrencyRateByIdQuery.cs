using MediatR;
using System;

namespace WebScrapperCurrencyRate.Application.CurrencyRates.Queries.GetCurrencyRateByDate
{ 
    public class GetCurrencyAverageRateByDateQuery : IRequest<CurrencyAverageRateDto>
    {
        public string Currency { get; set; }

        private DateTime _from { get; set; }
        public DateTime From
        {
            get
            {
                return _from;
            }
            set
            {
                _from = value.Date;
            }
        } 

        private DateTime _to { get; set; } 
        public DateTime To
        {
            get
            {
                return _to;
            }
            set
            {
                _to = value.Date;
            }
        }
    }
}