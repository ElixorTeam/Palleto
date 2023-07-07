// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.Access;

/// <summary>
/// Table validation "ACCESS".
/// </summary>
public sealed class WsSqlAccessValidator : WsSqlTableValidator<WsSqlAccessModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="isCheckIdentity"></param>
    public WsSqlAccessValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.LoginDt)
            .NotEmpty()
            .NotNull()
            .GreaterThanOrEqualTo(new DateTime(2000, 01, 01));
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Rights)
            .NotNull()
            .LessThanOrEqualTo((byte)WsEnumAccessRights.Admin)
            .GreaterThanOrEqualTo((byte)WsEnumAccessRights.None);
    }
}
