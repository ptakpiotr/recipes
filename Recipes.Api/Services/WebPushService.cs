using System.Text;
using Microsoft.Extensions.Options;
using Recipes.Infrastructure.Common.Options;
using WebPush;

namespace Recipes.Api.Services;

public class WebPushService(IOptionsSnapshot<VapidOptions> options, IOptionsSnapshot<FrontendOptions> frontendOptions) : IWebPushService
{
    public async Task SendPushNotificationAsync(string message, CancellationToken token)
    {
        var webPushClient = new WebPushClient();

        var pushSubscription = new PushSubscription(
            frontendOptions.Value.Url,
            options.Value.PublicKey,
            options.Value.PrivateKey
        );

        await webPushClient.SendNotificationAsync(pushSubscription,
                Convert.ToBase64String(Encoding.UTF8.GetBytes(message)), [], token)
            .ConfigureAwait(ConfigureAwaitOptions.None);
    }
}