using FluentValidation;
using Pl.Admin.Api.App.Features.References.Templates.Impl.Extensions;
using Pl.Admin.Models.Features.References.Template.Universal;
using Pl.Print.Features.Barcodes.Models;

namespace Pl.Admin.Api.App.Features.References.Templates.Impl.Validators;

internal sealed class BarcodeItemWrapperValidator : AbstractValidator<BarcodeItemWrapper>
{
    public BarcodeItemWrapperValidator()
    {
        RuleForEach(i => i.Top.ToBarcodeVar()).SetValidator(new BarcodeVarValidator())
            .OverridePropertyName(nameof(BarcodeItemWrapper.Top));
        RuleForEach(i => i.Bottom.ToBarcodeVar()).SetValidator(new BarcodeVarValidator())
            .OverridePropertyName(nameof(BarcodeItemWrapper.Bottom));
        RuleForEach(i => i.Right.ToBarcodeVar()).SetValidator(new BarcodeVarValidator())
            .OverridePropertyName(nameof(BarcodeItemWrapper.Right));
    }
}