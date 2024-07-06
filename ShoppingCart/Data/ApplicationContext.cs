using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using ShoppingCart.Domain;
using ShoppingCart.Utils;

namespace ShoppingCart.Data
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
                BsonClassMap.RegisterClassMap<Cart>(cm => cm.AutoMap());
            }

        }
       

    }
}
