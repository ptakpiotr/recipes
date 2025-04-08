using Microsoft.EntityFrameworkCore;
using Recipes.Application.Users.DTO;
using Recipes.Application.Users.Repositories;
using Recipes.Domain.Common.Enums;
using Recipes.Domain.Users.Enums;
using Recipes.Domain.Users.Models;
using Recipes.Infrastructure.Common.Data;

namespace Recipes.Infrastructure.Users.Repositories;

public class UsersRepository(AppDbContext ctx) : IUsersRepository
{
    public Task<UserModel?> GetUserByIdAsync(Guid userId, CancellationToken token)
    {
        return ctx.Users.Include(u => u.Roles).AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId, token);
    }

    public Task<UserModel?> GetUserByExternalIdAsync(string externalId, CancellationToken token)
    {
        return ctx.Users.Include(u => u.Roles).AsNoTracking()
            .FirstOrDefaultAsync(u => u.ExternalId == externalId, token);
    }

    public async Task<IList<UserModel>> GetUsersAsync(CancellationToken token)
    {
        return await ctx.Users.AsNoTracking().ToListAsync(token).ConfigureAwait(false);
    }

    public async Task<IList<UserModel>> GetUsersForNewseletterAsync(CancellationToken token)
    {
        return await ctx.Users.Where(x => x.SendNewsletter).AsNoTracking().ToListAsync(token).ConfigureAwait(false);
    }

    public async Task<UserModel?> CreateUserAsync(UserModel user, CancellationToken token)
    {
        user.Roles =
        [
            new RoleModel()
            {
                Name = "Użytkownik",
                Role = RoleType.User
            }
        ];
        await ctx.Users.AddAsync(user, token).ConfigureAwait(false);
        return user;
    }

    public async Task<UpdateType> UpdateUserAsync(UserEditDto user, CancellationToken token)
    {
        var userForModification =
            await ctx.Users.FirstOrDefaultAsync(u => u.Id == user.Id, token).ConfigureAwait(false);

        if (userForModification is null)
        {
            return UpdateType.UpdateFailed;
        }

        if (user.UserImageLink is { } userImageLink)
        {
            userForModification.UserImageLink = userImageLink;
        }

        if (user.SendNewsletter is { } sendNewsletter)
        {
            userForModification.SendNewsletter = sendNewsletter;
        }

        return UpdateType.UpdateSuccessful;
    }

    public async Task<DeleteType> DeleteUserAsync(Guid userId, CancellationToken token)
    {
        var userForDeletion =
            await ctx.Users.FirstOrDefaultAsync(u => u.Id == userId, token).ConfigureAwait(false);

        if (userForDeletion is null)
        {
            return DeleteType.DeleteFailed;
        }

        ctx.Remove(userForDeletion);

        return DeleteType.DeleteSuccessful;
    }

    public async Task<bool> CheckIfUserAdminAsync(Guid userId, CancellationToken token)
    {
        var user = await ctx.Users.Include(u => u.Roles)
            .FirstOrDefaultAsync(x => x.Id == userId, token)
            .ConfigureAwait(false);

        return user?.Roles.Any(x => x.Role == RoleType.Admin) == true;
    }

    public Task SaveChangesAsync(CancellationToken token)
    {
        return ctx.SaveChangesAsync(token);
    }
}