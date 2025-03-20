using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Recipes.Application.Users.Services;
using Recipes.Infrastructure.Common.Helpers;
using Recipes.Infrastructure.Common.Options;

namespace Recipes.Infrastructure.Common.Identity;

public static class ManageIdentityConfiguration
{
    public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        AuthOptions authOptions = new();

        configuration.GetSection("Auth").Bind(authOptions);

        services.AddAuthentication()
            .AddCookie(IdentityConstants.CookieAuthScheme)
            .AddOAuth(IdentityConstants.GithubAuthScheme, opts =>
            {
                opts.SignInScheme = IdentityConstants.CookieAuthScheme;
                opts.ClientId = authOptions.ClientId;
                opts.ClientSecret = authOptions.ClientSecret;
                opts.TokenEndpoint = authOptions.TokenEndpoint;
                opts.AuthorizationEndpoint = authOptions.AuthorizationEndpoint;
                opts.SaveTokens = true;
                opts.CallbackPath = "/cb/github";
                opts.AccessDeniedPath = "/login";

                opts.Events.OnCreatingTicket = async (ctx) =>
                {
                    var token = ctx.AccessToken;
                    using HttpRequestMessage msg = new()
                    {
                        RequestUri = new Uri(authOptions.UserEndpoint)
                    };
                    msg.Headers.Add("Authorization", $"Bearer {token}");

                    var resp = await ctx.Backchannel.SendAsync(msg);
                    var val = await resp.Content.ReadAsStringAsync();

                    var userInfo = JsonSerializer.Deserialize<Dictionary<string, object>>(val);

                    var appUser = ctx.Principal?.Identities.FirstOrDefault();

                    appUser.ApplyClaimsToExistingUser(userInfo);

                    var userService = ctx.HttpContext.RequestServices.GetRequiredService<IUserService>();

                    var externalId = userInfo?["id"].ToString();

                    var user = await userService
                        .GetUserByExternalIdAsync(externalId ?? string.Empty, CancellationToken.None)
                        .ConfigureAwait(ConfigureAwaitOptions.None);

                    if (user.IsT1)
                    {
                        await userService.CreateUserAsync(new()
                        {
                            ExternalId = externalId!,
                            SendNewsletter = false,
                            UserEmail = string.Empty,
                            UserName = userInfo["login"].ToString(),
                            UserImageLink = userInfo["avatar_url"].ToString()
                        }, CancellationToken.None).ConfigureAwait(ConfigureAwaitOptions.None);
                    }

                    var existingUser = await userService
                        .GetUserByExternalIdAsync(externalId ?? string.Empty, CancellationToken.None)
                        .ConfigureAwait(ConfigureAwaitOptions.None);

                    ctx.HttpContext.Items["user-id"] = existingUser.AsT0.Value.Id;

                    ctx.Response.Cookies.Append("user-login", userInfo["login"].ToString(), new()
                    {
                        HttpOnly = true,
                        Secure = true
                    });

                    ctx.Success();
                };
            });

        services.AddAuthorization((opts) =>
        {
            opts.AddPolicy(IdentityConstants.AuthzPolicy,
                pb => { pb.RequireAuthenticatedUser().AddAuthenticationSchemes(IdentityConstants.GithubAuthScheme); });

            opts.AddPolicy(IdentityConstants.AdminPolicy,
                pb =>
                {
                    pb.RequireAuthenticatedUser().RequireRole("Admin")
                        .AddAuthenticationSchemes(IdentityConstants.GithubAuthScheme);
                });
        });

        return services;
    }
}