using Recipes.Application.Users.Queries;
using Recipes.Application.Users.Services;

namespace Recipes.Application.Users.Handlers;

internal class GetRolesHandler(IRoleService roleService)
    : IRequestHandler<GetRolesQuery, OneOf<SuccessWithValue<IReadOnlyList<RoleReadDto>>, Error>>
{
    public Task<OneOf<SuccessWithValue<IReadOnlyList<RoleReadDto>>, Error>> Handle(GetRolesQuery request,
        CancellationToken cancellationToken)
    {
        return roleService.GetAllRolesAsync(cancellationToken);
    }
}