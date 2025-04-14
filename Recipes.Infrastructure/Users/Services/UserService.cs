using System.Text.Json;
using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using OneOf;
using Recipes.Application.Users.DTO;
using Recipes.Application.Users.Repositories;
using Recipes.Application.Users.Services;
using Recipes.Domain.Common.Enums;
using Recipes.Domain.Common.Results;
using Recipes.Domain.Users.Models;

namespace Recipes.Infrastructure.Users.Services;

public class UserService(IUsersRepository usersRepository, IDistributedCache cache, IMapper mapper) : IUserService
{
    private const string UserCacheKeyPrefix = "User";

    public async Task<OneOf<SuccessWithValue<UserReadDto>, Error>> GetUserByIdAsync(Guid userId,
        CancellationToken token)
    {
        var cacheKey = $"{UserCacheKeyPrefix}_{userId}";
        var cacheStrResult = await cache.GetStringAsync(cacheKey, token);

        if (!string.IsNullOrEmpty(cacheStrResult))
        {
            var user = JsonSerializer.Deserialize<UserReadDto>(cacheStrResult);

            if (user is null)
            {
                return new Error(ErrorType.Parse, nameof(user));
            }

            return new SuccessWithValue<UserReadDto>(user);
        }

        var userFromDb = await usersRepository.GetUserByIdAsync(userId, token)
            .ConfigureAwait(ConfigureAwaitOptions.None);

        if (userFromDb is null)
        {
            return new Error(ErrorType.NotFound);
        }

        await cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(userFromDb), new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
        }, token);

        var result = mapper.Map<UserReadDto>(userFromDb);

        result.Roles = mapper.Map<ICollection<RoleReadDto>>(userFromDb.Roles);

        return new SuccessWithValue<UserReadDto>(result);
    }

    public async Task<OneOf<SuccessWithValue<UserReadDto>, Error>> GetUserByExternalIdAsync(string externalId,
        CancellationToken token)
    {
        var userFromDb = await usersRepository.GetUserByExternalIdAsync(externalId, token)
            .ConfigureAwait(ConfigureAwaitOptions.None);

        if (userFromDb is null)
        {
            return new Error(ErrorType.NotFound);
        }

        var result = mapper.Map<UserReadDto>(userFromDb);

        return new SuccessWithValue<UserReadDto>(result);
    }

    public async Task<OneOf<SuccessWithValue<IReadOnlyList<UserReadDto>>, Error>> GetUsersForNewseletterAsync(
        CancellationToken token)
    {
        var usersFromDb = await usersRepository.GetUsersForNewseletterAsync(token)
            .ConfigureAwait(ConfigureAwaitOptions.None);

        if (usersFromDb.Count == 0)
        {
            return new Error(ErrorType.NotFound);
        }

        var result = mapper.Map<IList<UserReadDto>>(usersFromDb);

        return new SuccessWithValue<IReadOnlyList<UserReadDto>>(result.AsReadOnly().ToList());
    }

    public async Task<OneOf<SuccessWithValue<bool>, Error>> CheckIfUserAdminAsync(Guid userId, CancellationToken token)
    {
        var isAdmin = await usersRepository.CheckIfUserAdminAsync(userId, token)
            .ConfigureAwait(ConfigureAwaitOptions.None);

        if (!isAdmin)
        {
            return new Error(ErrorType.OperationFailed);
        }

        return new SuccessWithValue<bool>(isAdmin);
    }

    public async Task<OneOf<SuccessWithValue<IReadOnlyList<UserReadDto>>, Error>> GetAllUsersAsync(
        CancellationToken token)
    {
        var users = await usersRepository.GetUsersAsync(token).ConfigureAwait(ConfigureAwaitOptions.None);

        if (users.Count == 0)
        {
            return new Error(ErrorType.NotFound);
        }

        var result = mapper.Map<IList<UserReadDto>>(users);

        return new SuccessWithValue<IReadOnlyList<UserReadDto>>(result.AsReadOnly().ToList());
    }

    public async Task<OneOf<SuccessWithValue<IReadOnlyList<UserBasicReadDto>>, Error>> GetBasicsUsersAsync(CancellationToken token)
    {
        var users = await usersRepository.GetUsersAsync(token).ConfigureAwait(ConfigureAwaitOptions.None);

        if (users.Count == 0)
        {
            return new Error(ErrorType.NotFound);
        }

        var result = mapper.Map<IList<UserBasicReadDto>>(users);

        return new SuccessWithValue<IReadOnlyList<UserBasicReadDto>>(result.AsReadOnly().ToList());
    }

    public async Task<OneOf<SuccessWithValue<UserReadDto>, Error>> CreateUserAsync(UserCreateDto user,
        CancellationToken token)
    {
        var userToCreate = mapper.Map<UserModel>(user);

        var createdUser = await usersRepository.CreateUserAsync(userToCreate, token)
            .ConfigureAwait(ConfigureAwaitOptions.None);

        await usersRepository.SaveChangesAsync(token).ConfigureAwait(ConfigureAwaitOptions.None);

        var result = mapper.Map<UserReadDto>(createdUser);

        return new SuccessWithValue<UserReadDto>(result);
    }

    public async Task<OneOf<Success, Error>> UpdateUserAsync(UserEditDto user, CancellationToken token)
    {
        var updateOperation =
            await usersRepository.UpdateUserAsync(user, token).ConfigureAwait(ConfigureAwaitOptions.None);

        await usersRepository.SaveChangesAsync(token).ConfigureAwait(ConfigureAwaitOptions.None);

        if (updateOperation == UpdateType.UpdateFailed)
        {
            return new Error(ErrorType.OperationFailed);
        }

        var cacheKey = $"{UserCacheKeyPrefix}_{user.Id}";

        await cache.RemoveAsync(cacheKey, token).ConfigureAwait(ConfigureAwaitOptions.None);

        return new Success();
    }

    public async Task<OneOf<Success, Error>> DeleteUserAsync(UserDeleteDto user, CancellationToken token)
    {
        var deleteOperation =
            await usersRepository.DeleteUserAsync(user.Id, token).ConfigureAwait(ConfigureAwaitOptions.None);

        await usersRepository.SaveChangesAsync(token).ConfigureAwait(ConfigureAwaitOptions.None);

        if (deleteOperation == DeleteType.DeleteFailed)
        {
            return new Error(ErrorType.OperationFailed);
        }

        var cacheKey = $"{UserCacheKeyPrefix}_{user.Id}";

        await cache.RemoveAsync(cacheKey, token).ConfigureAwait(ConfigureAwaitOptions.None);

        return new Success();
    }
}