namespace ProductCatalog.Utils
{
    public sealed class DataBaseSettings
    {
        public string Host { get; set; }
        public string DatabaseName { get; set; }
        public string NoSqlDataBase { get; set; }

        public string ConnectionString { get { return $"{Host + "/" + DatabaseName}"; } }
        public DataBaseSettings()
        {
            Host = string.Empty;
            DatabaseName = string.Empty;
            NoSqlDataBase = string.Empty;
        }
    }
}
