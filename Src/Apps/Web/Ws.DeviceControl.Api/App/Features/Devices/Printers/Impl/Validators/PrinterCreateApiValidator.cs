using Microsoft.Extensions.Localization;
using Ws.Database.Entities.Ref.Printers;
using Ws.DeviceControl.Api.App.Features.Devices.Printers.Impl.Expressions;
using Ws.DeviceControl.Api.App.Features.Devices.Printers.Impl.Models;
using Ws.DeviceControl.Api.App.Shared.Validators.Api;
using Ws.DeviceControl.Models.Features.Devices.Printers.Commands;
using Ws.Shared.Resources;

namespace Ws.DeviceControl.Api.App.Features.Devices.Printers.Impl.Validators;

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



