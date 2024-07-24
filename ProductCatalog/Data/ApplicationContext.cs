using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using ProductCatalog.Utils;
using MongoDB.Bson;
using ProductCatalog.Domain.Entities;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ProductCatalog.Data
{
    public class ApplicationContext
    {
        private static bool _MongoMapped = false;

        private readonly DataBaseSettings _baseSettings;

        private readonly IMongoClient Client;

        public readonly IMongoDatabase DataBase;


        public ApplicationContext(IOptions<DataBaseSettings> dataBaseSettings)
        {
            _baseSettings = dataBaseSettings.Value;
            Client = new MongoClient(MongoClientSettings.FromUrl(new MongoUrl(_baseSettings.ConnectionString)));
            DataBase = Client.GetDatabase(_baseSettings.DatabaseName);

            if (!HasConnected())
                throw new DBConnectionException($"Não foi possível conectar ao banco de dados {_baseSettings.DatabaseName}");

            if (!_MongoMapped)
            {
                BsonClassMap.RegisterClassMap<Product>(cm => cm.AutoMap());

            }

        }

        public HealthCheckResult CheckHealthResult()
        {
            if (!HasConnected()) return HealthCheckResult.Degraded();

            if (!HasCollection() || !HasEllements()) return HealthCheckResult.Unhealthy();

            return HealthCheckResult.Healthy();
        }

        public void SeedDatabaseIfEmpty()
        {
            string entity = $"c_{typeof(Product).Name.ToLower()}";

            if (HasCollection() || HasEllements()) return;

            DataBase.CreateCollection(entity);
            var collection = DataBase.GetCollection<Product>(entity);
            collection.InsertMany(getList());
        }

        private bool HasConnected()
        {
            try
            {
                DataBase.RunCommand<BsonDocument>(new BsonDocument { { "ping", 1 } });
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        private bool HasCollection()
        {

            string entity = $"c_{typeof(Product).Name.ToLower()}";

            var filter = new BsonDocument("name", entity);
            var options = new ListCollectionNamesOptions { Filter = filter };

            return DataBase.ListCollectionNames(options).Any();

        }

        private bool HasEllements()
        {
            string entity = $"c_{typeof(Product).Name.ToLower()}";
            return DataBase.GetCollection<Product>(entity).AsQueryable().Any();
        }


        private List<Product> getList()
        {

            return new List<Product>
            {

        new Product
        (

            Id : ObjectId.Parse("669279420a7cde38d4d0b9df"),
            Name : "Smartphone",
            Code : "SPH-123",
            Description : "O mais recente smartphone com câmera de alta resolução.",
            Price : new Money( Amount : 999.99m, Currency:"EUR" ),
            Available:true
        ),
        new Product
        (
            Id : ObjectId.Parse("669279420a7cde38d4d0b9e0"),
            Name : "Notebook",
            Code : "NBK-456",
            Description : "Um notebook versátil com processador rápido e SSD.",
            Price : new Money( Amount :1499.99m,Currency:"EUR"),
            Available:true

        ),
        new Product
        (
            Id : ObjectId.Parse("669279420a7cde38d4d0b9e1"),
            Name : "Fones de Ouvido",
            Code : "FNS-789",
            Description : "Cancelamento de ruído ativo e som de alta qualidade.",
            Price : new Money ( Amount : 199.99m, Currency : "EUR" ),
            Available:true
        ),
        new Product
        (
            Id : ObjectId.Parse("669279420a7cde38d4d0b9e2"),
            Name : "Smart TV",
            Code : "TV-012",
            Description : "Tela OLED de 55 polegadas com suporte a HDR.",
            Price : new Money ( Amount : 2999.99m, Currency : "EUR" )
            ,
            Available:true
        ),
        new Product
        (
            Id : ObjectId.Parse("669279420a7cde38d4d0b9e3"),
            Name : "Cafeteira",
            Code : "CFR-345",
            Description : "Prepare café expresso e café longo com facilidade.",
            Price : new Money ( Amount : 79.99m, Currency : "EUR" ),
            Available:true
        ),
        new Product
        (
            Id : ObjectId.Parse("669279420a7cde38d4d0b9e4"),
            Name : "Console de Videogame",
            Code : "CNL-001",
            Description : "Console de última geração com gráficos em 4K.",
            Price : new Money ( Amount : 1299.99m, Currency : "EUR" ),
            Available:true
        ),
        new Product
        (
            Id : ObjectId.Parse("669279420a7cde38d4d0b9e5"),
            Name : "Câmera DSLR",
            Code : "CAM-234",
            Description : "Câmera profissional com sensor de 24MP e lentes intercambiáveis.",
            Price : new Money ( Amount : 1799.99m, Currency : "EUR" ),
            Available:true
        ),
        new Product
        (
            Id : ObjectId.Parse("669279420a7cde38d4d0b9e6"),
            Name : "Tablet",
            Code : "TBL-567",
            Description : "Tablet com tela de alta resolução e suporte a caneta digital.",
            Price : new Money ( Amount : 599.99m, Currency : "EUR" ),
            Available:true
        ),
        new Product
        (
            Id : ObjectId.Parse("669279420a7cde38d4d0b9e7"),
            Name : "Impressora Multifuncional",
            Code : "IMP-890",
            Description : "Impressora colorida com scanner e copiadora integrados.",
            Price : new Money ( Amount : 349.99m, Currency : "EUR" ),
            Available:true
        ),
        new Product
        (
            Id : ObjectId.Parse("669279420a7cde38d4d0b9e8"),
            Name : "Liquidificador",
            Code : "LQD-678",
            Description : "Liquidificador potente com diversas velocidades e função pulsar.",
            Price : new Money ( Amount : 99.99m, Currency : "EUR" ),
            Available:true
        ),new Product
        (
            Id : ObjectId.Parse("669279420a7cde38d4d0b9e9"),
            Name : "Smartwatch",
            Code : "SWT-901",
            Description : "Smartwatch com monitor de frequência cardíaca e GPS integrado.",
            Price : new Money ( Amount : 299.99m, Currency : "EUR" ),
            Available:true
        ),
        new Product
        (
            Id : ObjectId.Parse("669279420a7cde38d4d0b9ea"),
            Name : "Auriculares sem fios",
            Code : "AUR-567",
            Description : "Auriculares sem fios com cancelamento de ruído ativo e som de alta fidelidade.",
            Price : new Money ( Amount : 149.99m, Currency : "EUR" ),
            Available:true
        ),
        new Product
        (
            Id : ObjectId.Parse("669279420a7cde38d4d0b9eb"),
            Name : "Aspirador Robotizado",
            Code : "ASP-234",
            Description : "Aspirador robotizado com mapeamento inteligente e função de limpeza a seco.",
            Price : new Money ( Amount : 699.99m, Currency : "EUR" ),
            Available:true
        ),
        new Product
        (
            Id : ObjectId.Parse("669279420a7cde38d4d0b9ec"),
            Name : "Teclado Mecânico para Gaming",
            Code : "KBD-789",
            Description : "Teclado mecânico RGB com switches Cherry MX e teclas programáveis.",
            Price : new Money ( Amount : 199.99m, Currency : "EUR" ),
            Available:false
        ),
        new Product
        (
            Id : ObjectId.Parse("66a13b855f3f242e5ba7372f"),
            Name : "Máquina de Lavar Roupa",
            Code : "MLR-456",
            Description : "Máquina de lavar roupa com capacidade para 10kg e programas de lavagem variados.",
            Price : new Money ( Amount : 799.99m, Currency : "EUR" ),
            Available:false
        )


            };
        }

    }
}
