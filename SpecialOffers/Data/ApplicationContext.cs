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
            var objectId = ObjectId.GenerateNewId();
            return new List<SpecialOffer>{
                new SpecialOffer(ObjectId.Empty, 1, DateTime.Parse("2020-06-16T20:13:53.6678934+00:00"), "SpecialOfferCreated" ,"Best deal ever!!!"),
                new SpecialOffer(ObjectId.Empty,2, DateTime.Parse("2020-06-16T20:14:22.6229836+00:00"), "SpecialOfferCreated", "Special offer - just for you"),
                new SpecialOffer(objectId,3, DateTime.Parse("2020-06-16T20:14:39.841415+00:00"), "SpecialOfferCreated", "Nice deal"),
                new SpecialOffer(ObjectId.Empty,4, DateTime.Parse("2020-06-16T20:14:47.3420926+00:00"), "SpecialOfferUpdated", "Nice deal - JUST GOT BETTER",objectId),
                new SpecialOffer(ObjectId.Empty,5, DateTime.Parse("2020-06-16T20:14:51.8986625+00:00"), "SpecialOfferRemoved", "Special offer - just for you")
            };

        }
    }


}
