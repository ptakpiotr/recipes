using Microsoft.EntityFrameworkCore;
using Recipes.Application.Users.Repositories;
using Recipes.Domain.Users.Models;
using Recipes.Infrastructure.Common.Data;

namespace Recipes.Infrastructure.Users.Repositories;

public class RolesRepository(AppDbContext ctx) : IRolesRepository
{
    public Task<RoleModel?> GetRoleByIdAsync(Guid roleId, CancellationToken token)
    {
        return ctx.Roles.AsNoTracking().FirstOrDefaultAsync(r => r.Id == roleId, token);
    }

    public async Task<IList<RoleModel>> GetRolesAsync(CancellationToken token)
    {
        return await ctx.Roles.AsNoTracking().ToListAsync(token).ConfigureAwait(false);
    }

    public async Task<RoleModel?> CreateRoleAsync(RoleModel role, CancellationToken token)
    {
        await ctx.Roles.AddAsync(role, token).ConfigureAwait(false);

        return role;
    }

    public Task SaveChangesAsync(CancellationToken token)
    {
        return ctx.SaveChangesAsync(token);
    }
}