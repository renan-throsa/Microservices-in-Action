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
                new SpecialOffer(ObjectId.Empty,DateTime.Today, DateTime.Today.AddDays(1),"Cyper Week - Appliances deal" ,"Best deal ever over electronics!!!",new HashSet<string>(){"669279420a7cde38d4d0b9e0","669279420a7cde38d4d0b9e6","669279420a7cde38d4d0b9e9"},0.05f),
                new SpecialOffer(ObjectId.Empty,DateTime.Today, DateTime.Today.AddDays(3), "Clean Week - Appliances deal", "Just for you ! Deals over home and kitchen appliances",new HashSet<string>(){"669279420a7cde38d4d0b9eb","669279420a7cde38d4d0b9ed"},0.05f),
                new SpecialOffer(ObjectId.Empty,DateTime.Today.AddDays(-4), DateTime.Today.AddDays(-1), "Office Week", "Renew you office !",new HashSet<string>(){"669279420a7cde38d4d0b9e7","669279420a7cde38d4d0b9e3"},0.10f)
            };

        }
    }


}
