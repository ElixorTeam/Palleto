namespace Pl.Admin.Client.Source.Shared.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string ShortName(this ClaimsPrincipal user)
    {
        string name = user.Claims.First(x => x.Type == ClaimTypes.GivenName).Value;
        string surname = user.Claims.First(x => x.Type == ClaimTypes.Surname).Value;

        if (string.IsNullOrWhiteSpace(surname))
            return name.Capitalize();

        string nameChar = string.IsNullOrWhiteSpace(name) ? "" : $"{char.ToUpper(name[0])}.";
        return $"{surname} {nameChar}".Capitalize();
    }
}