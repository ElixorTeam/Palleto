using FluentValidation;

namespace Pl.Admin.Api.App.Shared.Validators.Api;

public abstract class BaseApiValidator<TDto> where TDto : class
{
    protected async Task ValidateProperties(AbstractValidator<TDto> validator, TDto armCreateDto)
    {
        ValidationResult result = await validator.ValidateAsync(armCreateDto);

        if (!result.IsValid)
            throw new ApiInternalException
            {
                ErrorDisplayMessage = result.Errors[0].ToString(),
                StatusCode = HttpStatusCode.UnprocessableEntity
            };
    }
}