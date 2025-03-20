namespace Recipes.Infrastructure.Common.Identity;

public static class IdentityConstants
{
    public const string CookieAuthScheme = "CookiesAuth";
    public const string GithubAuthScheme = "GithubAuth";

    public const string AuthzPolicy = "AuthzPolicy";
    public const string AdminPolicy = "AdminPolicy";
    
    internal static readonly string[] UserInfo = ["id", "login", "avatar_url"];
}