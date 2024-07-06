using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using ProductCatalog.Utils;
using ProductCatalog.Domain;
using MongoDB.Bson;

namespace ProductCatalog.Data
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
                BsonClassMap.RegisterClassMap<Product>(cm => cm.AutoMap());

            }

        }

        public void SeedDatabaseIfEmpty()
        {
            string entity = $"c_{typeof(Product).Name.ToLower()}";

            var filter = new BsonDocument("name", entity);
            var options = new ListCollectionNamesOptions { Filter = filter };

            if (DataBase.ListCollectionNames(options).Any())
            {
                return;
            }

            DataBase.CreateCollection(entity);
            var collection = DataBase.GetCollection<Product>(entity);
            collection.InsertMany(getList());
        }

        private List<Product> getList()
        {
            return new List<Product>
        {
            new Product
            {
                Name = "Smartphone",
                Code = "SPH-123",
                Description = "O mais recente smartphone com câmera de alta resolução.",
                Price = new Money(){ Amount = 999.99m,Currency="EUR" }
            },
            new Product
            {
                Name = "Notebook",
                Code = "NBK-456",
                Description = "Um notebook versátil com processador rápido e SSD.",
                Price = new Money(){ Amount =1499.99m,Currency="EUR" }
            },
            new Product
            {
                Name = "Fones de Ouvido",
                Code = "FNS-789",
                Description = "Cancelamento de ruído ativo e som de alta qualidade.",
                Price = new Money() { Amount = 199.99m, Currency = "EUR" }
            },
            new Product
            {
                Name = "Smart TV",
                Code = "TV-012",
                Description = "Tela OLED de 55 polegadas com suporte a HDR.",
                Price = new Money() { Amount = 2999.99m, Currency = "EUR" }
            },
            new Product
            {
                Name = "Cafeteira",
                Code = "CFR-345",
                 Description = "Prepare café expresso e café longo com facilidade.",
                Price = new Money() { Amount = 79.99m, Currency = "EUR" }
            },
            new Product
    {
        Name = "Console de Videogame",
        Code = "CNL-001",
        Description = "Console de última geração com gráficos em 4K.",
        Price = new Money() { Amount = 1299.99m, Currency = "EUR" }
    },
    new Product
    {
        Name = "Câmera DSLR",
        Code = "CAM-234",
        Description = "Câmera profissional com sensor de 24MP e lentes intercambiáveis.",
        Price = new Money() { Amount = 1799.99m, Currency = "EUR" }
    },
    new Product
    {
        Name = "Tablet",
        Code = "TBL-567",
        Description = "Tablet com tela de alta resolução e suporte a caneta digital.",
        Price = new Money() { Amount = 599.99m, Currency = "EUR" }
    },
    new Product
    {
        Name = "Impressora Multifuncional",
        Code = "IMP-890",
        Description = "Impressora colorida com scanner e copiadora integrados.",
        Price = new Money() { Amount = 349.99m, Currency = "EUR" }
    },
    new Product
    {
        Name = "Liquidificador",
        Code = "LQD-678",
        Description = "Liquidificador potente com diversas velocidades e função pulsar.",
        Price = new Money() { Amount = 99.99m, Currency = "EUR" }
    },new Product
    {
        Name = "Smartwatch",
        Code = "SWT-901",
        Description = "Smartwatch com monitor de frequência cardíaca e GPS integrado.",
        Price = new Money() { Amount = 299.99m, Currency = "EUR" }
    },
    new Product
    {
        Name = "Auriculares sem fios",
        Code = "AUR-567",
        Description = "Auriculares sem fios com cancelamento de ruído ativo e som de alta fidelidade.",
        Price = new Money() { Amount = 149.99m, Currency = "EUR" }
    },
    new Product
    {
        Name = "Aspirador Robotizado",
        Code = "ASP-234",
        Description = "Aspirador robotizado com mapeamento inteligente e função de limpeza a seco.",
        Price = new Money() { Amount = 699.99m, Currency = "EUR" }
    },
    new Product
    {
        Name = "Teclado Mecânico para Gaming",
        Code = "KBD-789",
        Description = "Teclado mecânico RGB com switches Cherry MX e teclas programáveis.",
        Price = new Money() { Amount = 199.99m, Currency = "EUR" }
    },
    new Product
    {
        Name = "Máquina de Lavar Roupa",
        Code = "MLR-456",
        Description = "Máquina de lavar roupa com capacidade para 10kg e programas de lavagem variados.",
        Price = new Money() { Amount = 799.99m, Currency = "EUR" }
    }
        };
        }

    }
}
