using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace ShoppingCart.Data
{
    public class ApplicationContext
    {
        private static bool _MongoMapped = false;

        private readonly DataBaseSettings _baseSettings;

        private IMongoDatabase _dataBase;

        public IMongoDatabase DataBase
        {
            get { return _dataBase ?? (_dataBase = Client.GetDatabase(_baseSettings.Database)); }
        }

        private IMongoClient _client;

        public IMongoClient Client
        {
            get { return _client ??= new MongoClient(MongoClientSettings.FromUrl(new MongoUrl(_baseSettings.ConnectionString))); }
        }

        public ApplicationContext(IOptions<DataBaseSettings> dataBaseSettings)
        {
            _baseSettings = dataBaseSettings.Value;

            if (DataBase == null)
                throw new ArgumentException("Não foi possível conectar ao banco de dados.");

            if (!_MongoMapped)
            {
                BsonClassMap.RegisterClassMap<Domain.Cart>(cm => cm.AutoMap());

            }

        }
    }
}
