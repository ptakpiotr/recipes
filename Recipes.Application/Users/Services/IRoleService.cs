namespace Recipes.Application.Users.Services;

public interface IRoleService
{
    Task<OneOf<SuccessWithValue<RoleReadDto>, Error>> GetRoleByIdAsync(Guid roleId, CancellationToken token);

    Task<OneOf<SuccessWithValue<IReadOnlyList<RoleReadDto>>, Error>> GetAllRolesAsync(CancellationToken token);
    
    Task<OneOf<SuccessWithValue<RoleReadDto>,Error>> CreateRoleAsync(RoleCreateDto role, CancellationToken token);
}