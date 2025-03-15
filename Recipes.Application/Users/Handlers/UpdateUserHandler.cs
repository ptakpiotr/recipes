using Recipes.Application.Users.Commands;
using Recipes.Application.Users.Services;
using Recipes.Domain.Common.ValueObjects;

namespace Recipes.Application.Users.Handlers;

internal class UpdateUserHandler(IUserService userService)
    : IRequestHandler<UpdateUserCommand, OneOf<CommandStatus, Error>>
{
    public async Task<OneOf<CommandStatus, Error>> Handle(UpdateUserCommand request,
        CancellationToken cancellationToken)
    {
        var updateResult = await userService.UpdateUserAsync(request.User, cancellationToken).ConfigureAwait(ConfigureAwaitOptions.None);

        var res = updateResult.Match((_) => new CommandStatus(true), (_) => new CommandStatus(false));

        return res;
    }
}