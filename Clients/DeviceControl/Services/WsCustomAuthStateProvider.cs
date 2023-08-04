// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DeviceControl.Services;

public class WsCustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IWsUserRightsService _userRightsService;
    private readonly IMemoryCache _cache;

    public WsCustomAuthStateProvider(IHttpContextAccessor httpContextAccessor, IWsUserRightsService userRightsService, IMemoryCache cache)
    {
        _httpContextAccessor = httpContextAccessor;
        _userRightsService = userRightsService;
        _cache = cache;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        ClaimsPrincipal? user = _httpContextAccessor.HttpContext?.User;

        if (user?.Identity?.IsAuthenticated != true || user.Identity.Name == null)
            return new(new());

        ClaimsIdentity claimsIdentity = new(user.Claims, "Windows");

        if (!_cache.TryGetValue(user.Identity.Name, out List<string>? userRights))
        {
            userRights = await _userRightsService.GetUserRightsAsync(user.Identity.Name);
            MemoryCacheEntryOptions cacheLifTime = new()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2),
            };
            _cache.Set(user.Identity.Name, userRights, cacheLifTime);
        }
        if (userRights is not null)
            foreach (string right in userRights)
                claimsIdentity.AddClaim(new(ClaimTypes.Role, right));
        return new(new(claimsIdentity));
    }
}