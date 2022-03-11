
using WebScrapperCurrencyRate.Application.Common.Mappings;
using WebScrapperCurrencyRate.Domain.Entities;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebScrapperCurrencyRate.Application.CurrencyRates.Commands.CreateCurrencyRate
{ 
    public class CreateCurrencyRateCommand : IRequest<object>, IMapTo<CurrencyRate>
    {
        [Required]
        public string Currency { get; set; }

        [Required]
        public DateTime Date { get; set; } = default!;

        [Required]
        public TimeSpan Time { get; set; } = default!;

        [Required]
        public long Buy { get; set; } = default!;

        [Required]
        public long Sell { get; set; } = default!;

    }
}