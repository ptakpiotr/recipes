using FluentEmail.Core;
using FluentEmail.Core.Models;
using MassTransit;
using Recipes.Application.Recipes.Events;
using Recipes.Application.Users.Services;

namespace Recipes.Infrastructure.Users.Events;

public class SendNewsletterDataConsumer(IFluentEmail fluentEmail, IUserService userService)
    : IConsumer<SendNewsletterDataEvent>
{
    public async Task Consume(ConsumeContext<SendNewsletterDataEvent> context)
    {
        var users = await userService.GetUsersForNewseletterAsync(context.CancellationToken)
            .ConfigureAwait(ConfigureAwaitOptions.None);

        await fluentEmail
            .Subject("Your weekly recipe newsletter")
            .Body($"The recipe: {context.Message.Recipe.Title}, address: {context.Message.Recipe.Id}")
            .To(users.AsT0.Value.Select(s => new Address()
            {
                EmailAddress = s.UserEmail
            }).ToList())
            .SendAsync(context.CancellationToken)
            .ConfigureAwait(ConfigureAwaitOptions.None);
    }
}