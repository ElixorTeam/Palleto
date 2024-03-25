namespace Ws.Database.EntityFramework.Entities.Ref.PalletMen;

[Table(SqlTables.PalletMen)]
[Index(nameof(Name), nameof(Surname), nameof(Patronymic), Name = $"UQ_{SqlTables.PalletMen}_FIO", IsUnique = true)]
[Index(nameof(Uid1C), Name = $"UQ_{SqlTables.PalletMen}_UID_1C", IsUnique = true)]
public sealed class PalletManEntity : EfEntityBase
{
    [Column(SqlColumns.Uid1C)]
    public Guid Uid1C { get; set; }

    [Column(SqlColumns.Name)]
    [StringLength(32, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 32 characters")]
    public string Name { get; set; } = string.Empty;

    [Column("SURNAME")]
    [StringLength(32, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 32 characters")]
    public string Surname { get; set; } = string.Empty;

    [Column("PATRONYMIC")]
    [StringLength(32, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 32 characters")]
    public string Patronymic { get; set; } = string.Empty;

    [Column("PASSWORD")]
    [StringLength(4, MinimumLength = 4, ErrorMessage = "Name must be between 4 characters")]
    public string Password { get; set; } = string.Empty;

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion

    // public virtual ICollection<Pallet> Pallets { get; set; } = new List<Pallet>();
}