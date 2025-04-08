using Recipes.Application.Users.Queries;
using Recipes.Application.Users.Services;

namespace Recipes.Application.Users.Handlers;

internal class CheckIfAdminHandler(IUserService userService)
    : IRequestHandler<CheckIfAdminQuery, OneOf<SuccessWithValue<bool>, Error>>
{
    public Task<OneOf<SuccessWithValue<bool>, Error>> Handle(CheckIfAdminQuery request,
        CancellationToken cancellationToken)
    {
        return userService.CheckIfUserAdminAsync(request.UserId, cancellationToken);
    }
}