using Microsoft.Extensions.Localization;
using Pl.Admin.Api.App.Features.Devices.Printers.Impl.Expressions;
using Pl.Admin.Api.App.Features.Devices.Printers.Impl.Models;
using Pl.Admin.Api.App.Shared.Validators.Api;
using Pl.Database.Entities.Ref.Printers;
using Pl.Admin.Models.Features.Devices.Printers.Commands;
using Pl.Shared.Resources;

namespace Pl.Admin.Api.App.Features.Devices.Printers.Impl.Validators;

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