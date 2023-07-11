namespace PersonalSoft.Domain.Settings
{
    public class MongoSettings
    {
        public string ServerName { get; set; } = string.Empty;
        public string Database { get; set; } = string.Empty;
        public MongoCollections MongoCollections { get; set; } = new();
    }

    public class MongoCollections
    {
        public string Cliente { get; set; } = string.Empty;
        public string PlanPoliza { get; set; } = string.Empty;
        public string Poliza { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;
    }
}
