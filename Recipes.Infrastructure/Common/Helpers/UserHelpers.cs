using System.Security.Claims;
using Recipes.Application.Users.DTO;
using Recipes.Infrastructure.Common.Identity;

namespace Recipes.Infrastructure.Common.Helpers;

public static class UserHelpers
{
    internal static void ApplyClaimsToExistingUser(this ClaimsIdentity? appUser, Dictionary<string, object>? userInfo)
    {
        ArgumentNullException.ThrowIfNull(appUser);
        ArgumentNullException.ThrowIfNull(userInfo);

        foreach (var i in IdentityConstants.UserInfo)
        {
            if (userInfo.TryGetValue(i, value: out var value))
            {
                appUser.AddClaim(new Claim(i, value.ToString()!));
            }
        }
    }

    internal static void ApplyRolesToExistingUser(this ClaimsIdentity? appUser, UserReadDto? userInfo)
    {
        ArgumentNullException.ThrowIfNull(appUser);
        ArgumentNullException.ThrowIfNull(userInfo);

        foreach (var role in userInfo.Roles)
        {
            appUser.AddClaim(new Claim(ClaimTypes.Role, role.Role.ToString()));
        }
    }
}