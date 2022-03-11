using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace WebScrapperCurrencyRate.Domain.Entities
{
    public class CurrencyRate
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
         
        public string Currency { get; set; }

        public DateTime Date { get; set; } = default!;

        public TimeSpan Time { get; set; } = default!;

        public long Buy { get; set; } = default!;

        public long Sell { get; set; } = default!;
    }

}



