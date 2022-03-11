using System;
using System.Net;

namespace WebScrapperCurrencyRate.Application.Common.Exceptions
{
    public class RestException : Exception
    { 
        public HttpStatusCode Code { get; }

        public new object Message { get; set; }


        public RestException()
            :base()
        {
            Code = HttpStatusCode.BadRequest;
        }

        public RestException(object message = default!)
        {
            Code = HttpStatusCode.BadRequest;
            Message = message;
        }

        public RestException(HttpStatusCode code, object message = default!)
        {
            Code = code;
            Message = message;
        }

        public RestException(HttpStatusCode code, string message, Exception innerException)
            : base(message, innerException)
        {
            Code = code;
            Message = message;
        } 
    }
}
