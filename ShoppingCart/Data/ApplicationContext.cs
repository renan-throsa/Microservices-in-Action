using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using ShoppingCart.Domain.Entities;
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

        public void SeedDatabaseIfEmpty()
        {
            string entity;
            entity = $"c_{typeof(Cart).Name.ToLower()}";

            if (!HasCollection(entity) || !HasEllements<Cart>(entity))
            {
                DataBase.CreateCollection(entity);
                var collection = DataBase.GetCollection<Cart>(entity);
                collection.InsertMany(getCartList());
            }

            entity = $"c_{typeof(Event).Name.ToLower()}";

            if (!HasCollection(entity) || !HasEllements<Event>(entity))
            {
                DataBase.CreateCollection(entity);
                var collection = DataBase.GetCollection<Event>(entity);
                collection.InsertMany(getEventList());
            }


        }


        private bool HasCollection(string collection)
        {

            var filter = new BsonDocument("name", collection);
            var options = new ListCollectionNamesOptions { Filter = filter };

            return DataBase.ListCollectionNames(options).Any();

        }

        private bool HasEllements<T>(string collection)
        {
            return DataBase.GetCollection<T>(collection).AsQueryable().Any();
        }

        private List<Cart> getCartList()
        {
            var items = new HashSet<CartItem>()
            {
                new CartItem(new ObjectId("669279420a7cde38d4d0b9eb"),1,"Aspirador Robotizado","Aspirador robotizado com mapeamento inteligente e função de limpeza a seco.",new Money("EUR",899.99m)),
                new CartItem(new ObjectId("66a13b855f3f242e5ba7372f"),1,"Máquina de Lavar Roupa","Máquina de lavar roupa com capacidade para 10kg e programas de lavagem variados.",new Money("EUR",799.99m)),
                new CartItem(new ObjectId("669279420a7cde38d4d0b9e8"),1,"Liquidificador","Liquidificador potente com diversas velocidades e função pulsar.",new Money("EUR",99.99m))

            };
            
            return new List<Cart>
            {
                new Cart() {
                    Id = new ObjectId("6696b734c876203786c48c34"),
                    UserId = new ObjectId("668aba9950056742c7e3704d"),
                    Items = items
                }
            };
        }

        private List<Event> getEventList()
        {
            return new List<Event>()
            {
                new Event(new ObjectId("6696b734c876203786c48c31"),new ObjectId("668aba9950056742c7e3704d"),new ObjectId("669279420a7cde38d4d0b9eb"),1,DateTime.Parse("2024-07-16T18:08:52.176+00:00"),"ShoppingCartItemAdded"),
                new Event(new ObjectId("6696b734c876203786c48c32"),new ObjectId("668aba9950056742c7e3704d"),new ObjectId("66a13b855f3f242e5ba7372f"),2,DateTime.Parse("2024-07-16T18:08:52.233+00:00"),"ShoppingCartItemAdded"),
                new Event(new ObjectId("6696b734c876203786c48c33"),new ObjectId("668aba9950056742c7e3704d"),new ObjectId("669279420a7cde38d4d0b9e8"),3,DateTime.Parse("2024-07-16T18:08:52.235+00:00"),"ShoppingCartItemAdded"),
            };
        }

    }
}
