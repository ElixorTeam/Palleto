using Microsoft.Extensions.Localization;
using Pl.Admin.Api.App.Features.Devices.Printers.Impl.Expressions;
using Pl.Admin.Api.App.Features.Devices.Printers.Impl.Models;
using Pl.Admin.Api.App.Shared.Validators.Api;
using Pl.Database.Entities.Ref.Printers;
using Pl.Admin.Models.Features.Devices.Printers.Commands;
using Pl.Shared.Resources;

namespace Pl.Admin.Api.App.Features.Devices.Printers.Impl.Validators;

public class PrinterCreateApiValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    : ApiCreateValidator<PrinterEntity, PrinterCreateDto>
{
    public override async Task ValidateAsync(DbSet<PrinterEntity> dbSet, PrinterCreateDto dto)
    {
        UqPrinterProperties uqProperties = new(dto.Ip, dto.Name);
        await ValidateProperties(new PrinterCreateValidator(wsDataLocalizer), dto);
        await ValidatePredicatesAsync(dbSet, PrinterExpressions.GetUqPredicates(uqProperties));
    }
}



