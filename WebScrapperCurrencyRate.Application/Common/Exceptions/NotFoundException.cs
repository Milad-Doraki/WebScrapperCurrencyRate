using System;
using System.Net;

namespace WebScrapperCurrencyRate.Application.Common.Exceptions
{

    public class NotFoundException : RestException
    {
        public NotFoundException()
            : base(HttpStatusCode.NotFound)
        {
        }

        public NotFoundException(string message)
            : base(HttpStatusCode.NotFound, message)
        {
        }

        public NotFoundException(string message, Exception innerException)
            : base(HttpStatusCode.NotFound, message, innerException)
        {
        }

        public NotFoundException(string name, object key)
            : base(HttpStatusCode.NotFound, $"Entity \"{name}\" ({key}) was not found.")
        {
        }
    }
}

