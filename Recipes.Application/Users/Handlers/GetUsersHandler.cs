using Recipes.Application.Users.Queries;
using Recipes.Application.Users.Services;

namespace Recipes.Application.Users.Handlers;

internal class GetUsersHandler(IUserService userService) : IRequestHandler<GetUsersQuery, OneOf<SuccessWithValue<IReadOnlyList<UserReadDto>>, Error>>
{
    public Task<OneOf<SuccessWithValue<IReadOnlyList<UserReadDto>>, Error>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return userService.GetAllUsersAsync(cancellationToken); 
    }
}