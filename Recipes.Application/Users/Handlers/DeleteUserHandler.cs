using Recipes.Application.Users.Commands;
using Recipes.Application.Users.Services;
using Recipes.Domain.Common.ValueObjects;

namespace Recipes.Application.Users.Handlers;

internal class DeleteUserHandler(IUserService userService)
    : IRequestHandler<DeleteUserCommand, OneOf<CommandStatus, Error>>
{
    public async Task<OneOf<CommandStatus, Error>> Handle(DeleteUserCommand request,
        CancellationToken cancellationToken)
    {
        var deleteResult = await userService.DeleteUserAsync(request.User, cancellationToken)
            .ConfigureAwait(ConfigureAwaitOptions.None);

        var res = deleteResult.Match((_) => new CommandStatus(true), (_) => new CommandStatus(false));

        return res;
    }
}