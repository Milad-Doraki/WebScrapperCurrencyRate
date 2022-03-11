using System;

namespace WebScrapperCurrencyRate.Application.Common.Exceptions
{
    public class ForbiddenAccessException : Exception
    {
        public ForbiddenAccessException() : base() { }
    }
}
