using AutoMapper;
using WebScrapperCurrencyRate.Application.Common.Exceptions;
using WebScrapperCurrencyRate.Application.Common.Interfaces;
using WebScrapperCurrencyRate.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace WebScrapperCurrencyRate.Application.CurrencyRates.Queries.GetCurrencyRateByDate
{
    public class GetCurrencyAverageRateByDateQueryHandler : IRequestHandler<GetCurrencyAverageRateByDateQuery, CurrencyAverageRateDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCurrencyAverageRateByDateQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CurrencyAverageRateDto> Handle(GetCurrencyAverageRateByDateQuery request, CancellationToken cancellationToken)
        {
            var currencyRateList = await _context.FindListAsync<CurrencyRate>(c => c.Currency == request.Currency && request.From <= c.Date && c.Date <= request.To);
           
            return new CurrencyAverageRateDto
            {
                AverageBuy = (long?)(currencyRateList.DefaultIfEmpty().Average(c => c?.Buy)),
                AverageSell = (long?)(currencyRateList.DefaultIfEmpty().Average(c => c?.Sell))
            };
        }
    }
}