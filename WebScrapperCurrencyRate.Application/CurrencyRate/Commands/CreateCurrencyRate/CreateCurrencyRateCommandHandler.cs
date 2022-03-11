using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebScrapperCurrencyRate.Application.Common.Interfaces;
using WebScrapperCurrencyRate.Domain.Entities;
using System.Linq;
using WebScrapperCurrencyRate.Application.Common.Exceptions;
using System.Net;

namespace WebScrapperCurrencyRate.Application.CurrencyRates.Commands.CreateCurrencyRate
{
    public class CreateCurrencyRateCommandHandler : IRequestHandler<CreateCurrencyRateCommand, object>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateCurrencyRateCommandHandler(IApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<object> Handle(CreateCurrencyRateCommand createCurrencyRateCommand, CancellationToken cancellationToken)
        {
            var currencyRate = _mapper.Map<CurrencyRate>(createCurrencyRateCommand);

            if(_context.Any<CurrencyRate>(c => c.Currency == currencyRate.Currency && c.Date == currencyRate.Date && c.Time == currencyRate.Time))
            {
                throw new RestException("CurrencyRates must be unique in database: By Date and Time."); 
            }
             
            await _context.AddAsync(currencyRate, cancellationToken); 

            return currencyRate.Id;
        }
    }
}