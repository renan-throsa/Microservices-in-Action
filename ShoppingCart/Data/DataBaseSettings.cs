namespace ShoppingCart.Data
{
    public sealed class DataBaseSettings
    {
        public string Host { get; set; }
        public string Database { get; set; }
        public string NoSqlDataBase { get; set; }

        public string ConnectionString { get { return $"{Host + "/" + Database}"; } }
        public DataBaseSettings()
        {
            Host = string.Empty;
            Database = string.Empty;
            NoSqlDataBase = string.Empty;
        }
    }
}
