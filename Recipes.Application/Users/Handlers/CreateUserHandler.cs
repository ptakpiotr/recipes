using Recipes.Application.Users.Commands;
using Recipes.Application.Users.Services;

namespace Recipes.Application.Users.Handlers;

internal class CreateUserHandler(IUserService userService) : IRequestHandler<CreateUserCommand, OneOf<SuccessWithValue<UserReadDto>, Error>>
{
    public Task<OneOf<SuccessWithValue<UserReadDto>, Error>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        return userService.CreateUserAsync(request.User, cancellationToken);
    }
}