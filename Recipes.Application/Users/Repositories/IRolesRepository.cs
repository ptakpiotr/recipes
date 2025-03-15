namespace Recipes.Application.Users.Repositories;

public interface IRolesRepository
{
    Task<RoleModel?> GetRoleByIdAsync(Guid roleId, CancellationToken token);
    
    Task<IList<RoleModel>> GetRolesAsync(CancellationToken token);
    
    Task<RoleModel?> CreateRoleAsync(RoleModel role, CancellationToken token);
    
    Task SaveChangesAsync(CancellationToken token);
}