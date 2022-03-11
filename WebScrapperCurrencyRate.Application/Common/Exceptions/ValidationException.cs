using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebScrapperCurrencyRate.Application.Common.Exceptions
{ 
    public class ValidationException : RestException
    {
        public ValidationException()
            : base("One or more validation failures have occurred.")
        {    
        }

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Message = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray()); 
        } 
    }

}
