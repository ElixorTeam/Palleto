namespace Pl.Database.Shared.Models;

internal class SqlSettingsModel
{
    public string DataSource { get; set; } = string.Empty;
    public string InitialCatalog { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool PersistSecurityInfo { get; set; }
    public bool IsShowSql { get; set; }
    public bool TrustServerCertificate { get; set; }
    public bool IntegratedSecurity { get; set; }

    public string GetConnectionString() =>
        $"Data Source={DataSource}; " +
        $"Initial Catalog={InitialCatalog}; " +
        $"User ID={UserId}; " +
        $"Password={Password}; " +
        $"TrustServerCertificate={TrustServerCertificate}; " +
        $"Persist Security Info={PersistSecurityInfo};" +
        $"Integrated Security={IntegratedSecurity}; ";
}