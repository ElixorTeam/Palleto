namespace Pl.Database.Options;

public class DatabaseOptions
{
    public const string Database = "Database";

    public bool IsShowSql { get; set; }
    public string ConnectionString { get; set; } = string.Empty;
}