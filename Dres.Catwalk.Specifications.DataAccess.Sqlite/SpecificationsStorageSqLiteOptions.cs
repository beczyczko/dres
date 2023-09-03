namespace Dres.Catwalk.Specifications.DataAccess.Sqlite;

public class SpecificationsStorageSqLiteOptions
{
    public const string Position = "SpecificationsStorage:SqLite";
    
    public string ConnectionString { get; set; } = null!;
}