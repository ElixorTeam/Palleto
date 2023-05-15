// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.Devices;

/// <summary>
/// Table validation "DEVICES".
/// </summary>
public sealed class WsSqlDeviceValidator : WsSqlTableValidator<WsSqlDeviceModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlDeviceValidator() : base(true, true)
    {
        RuleFor(item => item.LoginDt)
            .NotEmpty()
            .NotNull()
            .LessThanOrEqualTo(DateTime.Now.Date.AddDays(1));
        RuleFor(item => item.LogoutDt)
            .NotEmpty()
            .NotNull()
            .LessThanOrEqualTo(DateTime.Now.Date.AddDays(1));
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.PrettyName)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Description)
            .NotNull();
        RuleFor(item => item.Ipv4)
           .NotNull();
        RuleFor(item => item.MacAddressValue)
            .NotNull();
    }
}