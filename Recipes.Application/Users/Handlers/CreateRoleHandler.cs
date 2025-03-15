using Recipes.Application.Users.Commands;
using Recipes.Application.Users.Services;

namespace Recipes.Application.Users.Handlers;

internal class CreateRoleHandler(IRoleService roleService)
    : IRequestHandler<CreateRoleCommand, OneOf<SuccessWithValue<RoleReadDto>, Error>>
{
    public Task<OneOf<SuccessWithValue<RoleReadDto>, Error>> Handle(CreateRoleCommand request,
        CancellationToken cancellationToken)
    {
        return roleService.CreateRoleAsync(request.Role, cancellationToken);
    }
}