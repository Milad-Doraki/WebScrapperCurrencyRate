using FluentValidation;

namespace WebScrapperCurrencyRate.Application.Common.Extensions
{
    public static class FluentValidationRules
    {
        public static IRuleBuilderOptions<T, string> CheckNumber<T>(this IRuleBuilder<T, string> rule)
           => rule.Matches(@"^\d+$")
                  .WithMessage("Invalid number");
    }
}
