using Microsoft.EntityFrameworkCore;
using Recipes.Application.Users.DTO;
using Recipes.Application.Users.Repositories;
using Recipes.Domain.Common.Enums;
using Recipes.Domain.Users.Models;
using Recipes.Infrastructure.Common.Data;

namespace Recipes.Infrastructure.Users.Repositories;

public class UsersRepository(AppDbContext ctx) : IUsersRepository
{
    public Task<UserModel?> GetUserByIdAsync(Guid userId, CancellationToken token)
    {
        return ctx.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId, token);
    }

    public async Task<IList<UserModel>> GetUsersAsync(CancellationToken token)
    {
        return await ctx.Users.AsNoTracking().ToListAsync(token).ConfigureAwait(false);
    }

    public async Task<UserModel?> CreateUserAsync(UserModel user, CancellationToken token)
    {
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

        if (user.UserImageLink is {} userImageLink)
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

    public Task SaveChangesAsync(CancellationToken token)
    {
        return ctx.SaveChangesAsync(token);
    }
}