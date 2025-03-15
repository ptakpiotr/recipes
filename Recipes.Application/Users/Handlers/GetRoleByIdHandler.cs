using Recipes.Application.Users.Queries;
using Recipes.Application.Users.Services;

namespace Recipes.Application.Users.Handlers;

internal class GetRoleByIdHandler(IRoleService roleService) : IRequestHandler<GetRoleByIdQuery, OneOf<SuccessWithValue<RoleReadDto>, Error>>
{
    public Task<OneOf<SuccessWithValue<RoleReadDto>, Error>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        return roleService.GetRoleByIdAsync(request.RoleId, cancellationToken); 
    }
}