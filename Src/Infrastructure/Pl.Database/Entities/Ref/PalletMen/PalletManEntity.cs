namespace Pl.Database.Entities.Ref.PalletMen;

public sealed class PalletManEntity : EfEntityBase
{
    public Guid Uid1C { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Patronymic { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    #region Fk

    public WarehouseEntity Warehouse { get; set; } = null!;

    #endregion

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion
}