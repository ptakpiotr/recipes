using System.Text.Json;
using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using OneOf;
using Recipes.Application.Users.DTO;
using Recipes.Application.Users.Repositories;
using Recipes.Application.Users.Services;
using Recipes.Domain.Common.Enums;
using Recipes.Domain.Common.Results;
using Recipes.Domain.Users.Models;

namespace Recipes.Infrastructure.Users.Services;

public class RoleService(
    IRolesRepository rolesRepository,
    IDistributedCache cache,
    ILogger<RoleService> logger,
    IMapper mapper)
    : IRoleService
{
    private const string RoleCacheKeyPrefix = "Role";

    public async Task<OneOf<SuccessWithValue<RoleReadDto>, Error>> GetRoleByIdAsync(Guid roleId,
        CancellationToken token)
    {
        var cacheKey = $"{RoleCacheKeyPrefix}_{roleId}";
        var cacheStrResult = await cache.GetStringAsync(cacheKey, token);

        if (!string.IsNullOrEmpty(cacheStrResult))
        {
            var role = JsonSerializer.Deserialize<RoleReadDto>(cacheStrResult);

            if (role is null)
            {
                //TODO: consider using message builder
                return new Error(ErrorType.Parse, nameof(role));
            }

            return new SuccessWithValue<RoleReadDto>(role);
        }

        var roleFromDb = await rolesRepository.GetRoleByIdAsync(roleId, token)
            .ConfigureAwait(ConfigureAwaitOptions.None);

        if (roleFromDb is null)
        {
            return new Error(ErrorType.NotFound);
        }

        await cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(roleFromDb), new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
        }, token);

        var result = mapper.Map<RoleReadDto>(roleFromDb);

        return new SuccessWithValue<RoleReadDto>(result);
    }

    public async Task<OneOf<SuccessWithValue<IReadOnlyList<RoleReadDto>>, Error>> GetAllRolesAsync(
        CancellationToken token)
    {
        var roles = await rolesRepository.GetRolesAsync(token).ConfigureAwait(ConfigureAwaitOptions.None);

        if (roles.Count == 0)
        {
            return new Error(ErrorType.NotFound);
        }

        var result = mapper.Map<IList<RoleReadDto>>(roles);

        return new SuccessWithValue<IReadOnlyList<RoleReadDto>>(result.AsReadOnly().ToList());
    }

    public async Task<OneOf<SuccessWithValue<RoleReadDto>, Error>> CreateRoleAsync(RoleCreateDto role,
        CancellationToken token)
    {
        var roleToCreate = mapper.Map<RoleModel>(role);

        var createdRole = await rolesRepository.CreateRoleAsync(roleToCreate, token)
            .ConfigureAwait(ConfigureAwaitOptions.None);

        await rolesRepository.SaveChangesAsync(token).ConfigureAwait(ConfigureAwaitOptions.None);

        var result = mapper.Map<RoleReadDto>(createdRole);

        return new SuccessWithValue<RoleReadDto>(result);
    }
}