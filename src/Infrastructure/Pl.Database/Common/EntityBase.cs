using System.ComponentModel.DataAnnotations;

namespace Pl.Database.Common;

public abstract class EntityBase
{
    [Key]
    [Column(DbColumns.Uid)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
}