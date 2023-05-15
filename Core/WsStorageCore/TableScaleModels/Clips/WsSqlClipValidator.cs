// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.Clips;


/// <summary>
/// Table validation "CLIPS".
/// </summary>
public sealed class WsSqlClipValidator : WsSqlTableValidator<WsSqlClipModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlClipValidator() : base(true, true)
    {
        RuleFor(item => item.Name)
            .NotNull();
        RuleFor(item => item.Weight)
            .NotNull()
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(100);
    }
}