using System.Text;
using Microsoft.Extensions.Options;
using Recipes.Infrastructure.Common.Options;
using WebPush;

namespace Recipes.Api.Services;

public class WebPushService(IOptionsSnapshot<VapidOptions> options) : IWebPushService
{
    public async Task SendPushNotificationAsync(string message, CancellationToken token)
    {
        var webPushClient = new WebPushClient();

        var pushSubscription = new PushSubscription(
            "https://localhost:7151",
            options.Value.PublicKey,
            options.Value.PrivateKey
        );

        await webPushClient.SendNotificationAsync(pushSubscription,
                Convert.ToBase64String(Encoding.UTF8.GetBytes(message)), [], token)
            .ConfigureAwait(ConfigureAwaitOptions.None);
    }
}