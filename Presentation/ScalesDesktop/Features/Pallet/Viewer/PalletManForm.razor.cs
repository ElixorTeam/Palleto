using System.ComponentModel.DataAnnotations;
using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ScalesDesktop.Services;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.PalletMan;

namespace ScalesDesktop.Features.Pallet.Viewer;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed partial class PalletManForm : ComponentBase
{
    [Inject] private INotificationService NotificationService { get; set; } = null!;
    [Inject] private PalletContext PalletContext { get; set; } = null!;
    [Inject] private IPalletManService PalletManService { get; set; } = null!;
    
    [SupplyParameterFromForm] private PalletManFormModel FormModel { get; set; } = new();
    
    private IEnumerable<PalletManEntity> GetAllPalletMen() => PalletManService.GetAll();
    
    private void HandleInvalidForm(EditContext context)
    {
        foreach (string msg in context.GetValidationMessages())
            NotificationService.Error(msg);
    }

    private void OnSubmit()
    {
        PalletContext.SetPalletMan(FormModel.User!);
    } 
}

public class PalletManFormModel
{
    [Required(ErrorMessage = "Пользователь обязателен для заполнения")]
    public PalletManEntity? User { get; set; }
    
    [Required(ErrorMessage = "Пароль обязателен для заполнения")]
    [RegularExpression(@"\d{4}$", ErrorMessage = "Пароль должен состоять из 4 цифр")]
    public string Password { get; set; } = string.Empty;
}