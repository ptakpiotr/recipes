using Recipes.Application.Users.Queries;
using Recipes.Application.Users.Services;

namespace Recipes.Application.Users.Handlers;

public class GetBasicUsersHandler(IUserService userService) : IRequestHandler<GetBasicUsersQuery,
    OneOf<SuccessWithValue<IReadOnlyList<UserBasicReadDto>>, Error>>
{
    public Task<OneOf<SuccessWithValue<IReadOnlyList<UserBasicReadDto>>, Error>> Handle(GetBasicUsersQuery request,
        CancellationToken cancellationToken)
    {
        return userService.GetBasicsUsersAsync(cancellationToken);
    }
}