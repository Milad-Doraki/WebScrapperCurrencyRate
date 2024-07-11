using FluentValidation;
using WebScrapperCurrencyRate.Application.Common.Extensions;

namespace WebScrapperCurrencyRate.Application.CurrencyRates.Commands.CreateCurrencyRate
{
    public class CreateCurrencyRateCommandValidator : AbstractValidator<CreateCurrencyRateCommand>
    {
        public CreateCurrencyRateCommandValidator()
        {
            RuleFor(currencyRate => currencyRate.Currency).NotEmpty();
            RuleFor(currencyRate => currencyRate.Buy).GreaterThanOrEqualTo(0);
            RuleFor(currencyRate => currencyRate.Sell).GreaterThanOrEqualTo(0);

        }
    }
}