namespace Pl.Admin.Api.App.Shared.Exceptions;

public enum ApiErrorType
{
    [Description("errorNotFound")]
    NotFound,
    [Description("errorUnique")]
    Unique,
    [Description("errorIsUse")]
    IsUse
}

public class ApiInternalLocalizingException : Exception
{
    public required string PropertyName { get; init; }
    public required ApiErrorType ErrorType { get; init; }
}