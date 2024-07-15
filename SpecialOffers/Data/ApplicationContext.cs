using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using SpecialOffers.Domain.Entities;
using SpecialOffers.Utils;
namespace SpecialOffers.Data
{
    public class ApplicationContext
    {
        private static bool _MongoMapped = false;

        private readonly DataBaseSettings _baseSettings;

        public readonly IMongoDatabase DataBase;

        public readonly IMongoClient Client;

        public ApplicationContext(IOptions<DataBaseSettings> dataBaseSettings)
        {
            _baseSettings = dataBaseSettings.Value;
            Client = new MongoClient(MongoClientSettings.FromUrl(new MongoUrl(_baseSettings.ConnectionString)));
            DataBase = Client.GetDatabase(_baseSettings.Database);

            if (DataBase == null)
                throw new DBConnectionException($"Não foi possível conectar ao banco de dados {_baseSettings.Database}");

            if (!_MongoMapped)
            {
                BsonClassMap.RegisterClassMap<SpecialOffer>(cm => cm.AutoMap());
            }

        }

        public void SeedDatabaseIfEmpty()
        {
            string entity = $"c_{typeof(SpecialOffer).Name.ToLower()}";

            var filter = new BsonDocument("name", entity);
            var options = new ListCollectionNamesOptions { Filter = filter };

            if (DataBase.ListCollectionNames(options).Any())
            {
                return;
            }

            DataBase.CreateCollection(entity);
            var collection = DataBase.GetCollection<SpecialOffer>(entity);
            collection.InsertMany(getList());
        }

        private List<SpecialOffer> getList()
        {            
            return new List<SpecialOffer>{
                new SpecialOffer(ObjectId.Empty,DateTime.Today, DateTime.Today.AddDays(1),"SpecialOfferCreated" ,"Best deal ever!!!",new HashSet<string>(){"669279420a7cde38d4d0b9df"},0.05f),
                new SpecialOffer(ObjectId.Empty,DateTime.Today, DateTime.Today.AddDays(3), "SpecialOfferCreated", "Special offer - just for you",new HashSet<string>(){"669279420a7cde38d4d0b9e1"},0.05f),
                new SpecialOffer(ObjectId.Empty,DateTime.Today, DateTime.Today.AddDays(3), "SpecialOfferCreated", "Nice deal",new HashSet<string>(){"669279420a7cde38d4d0b9e0"},0.10f),
                new SpecialOffer(ObjectId.Empty,DateTime.Today.AddDays(-4), DateTime.Today.AddDays(-1), "SpecialOfferCreated", "Clean Week - Appliances deal",new HashSet<string>(){"669279420a7cde38d4d0b9ed"},0.10f)
            };

        }
    }


}
