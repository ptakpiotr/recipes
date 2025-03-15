using Recipes.Application.Users.Queries;
using Recipes.Application.Users.Services;

namespace Recipes.Application.Users.Handlers;

internal class GetUserByIdHandler(IUserService userService) : IRequestHandler<GetUserByIdQuery, OneOf<SuccessWithValue<UserReadDto>, Error>>
{
    public Task<OneOf<SuccessWithValue<UserReadDto>, Error>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        return userService.GetUserByIdAsync(request.UserId, cancellationToken); 
    }
}