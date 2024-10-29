using Microsoft.Extensions.Localization;
using Ws.Database.Entities.Ref.Printers;
using Ws.DeviceControl.Api.App.Features.Devices.Printers.Impl.Expressions;
using Ws.DeviceControl.Api.App.Features.Devices.Printers.Impl.Models;
using Ws.DeviceControl.Api.App.Shared.Validators.Api;
using Ws.DeviceControl.Models.Features.Devices.Printers.Commands;
using Ws.Shared.Resources;

namespace Ws.DeviceControl.Api.App.Features.Devices.Printers.Impl.Validators;

public class PrinterUpdateApiValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    : ApiUpdateValidator<PrinterEntity, PrinterUpdateDto, Guid>
{
    public override async Task<PrinterEntity> ValidateAndGetAsync(DbSet<PrinterEntity> dbSet, PrinterUpdateDto dto, Guid id)
    {
        UqPrinterProperties uqProperties = new(dto.Ip, dto.Name);
        await ValidateProperties(new PrinterUpdateValidator(wsDataLocalizer), dto);
        return await ValidatePredicatesAsync(dbSet, PrinterExpressions.GetUqPredicates(uqProperties), i => i.Id == id);
    }
}