namespace Recipes.Api.Services;

public interface IWebPushService
{
    Task SendPushNotificationAsync(string message, CancellationToken token);
}