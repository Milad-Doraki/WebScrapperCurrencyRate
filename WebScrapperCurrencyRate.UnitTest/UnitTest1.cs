using AutoMapper;
using FakeItEasy;
using MediatR;
using System;
using System.Threading.Tasks;
using WebScrapperCurrencyRate.Application.Common.Interfaces;
using WebScrapperCurrencyRate.Application.CurrencyRates.Commands.CreateCurrencyRate;
using Xunit;

namespace WebScrapperCurrencyRate.UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1Async()
        {
            // mock  
            var mapper = A.Fake<IMapper>(); 
            var context = A.Fake<IApplicationDbContext>(); 

            var handler = new CreateCurrencyRateCommandHandler(context, mapper);

            var command = new CreateCurrencyRateCommand()
            {
                Date = DateTime.Now,
                Time = DateTime.Now.TimeOfDay,
                Currency = "USD",
                Buy = 11,
                Sell= 12
            };
             
            var actualResult = await handler.Handle(command, new System.Threading.CancellationToken());

            Assert.NotNull(actualResult);
        }
    }
}
