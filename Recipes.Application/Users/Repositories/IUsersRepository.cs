using Recipes.Domain.Common.Enums;

namespace Recipes.Application.Users.Repositories;

public interface IUsersRepository
{
    Task<UserModel?> GetUserByIdAsync(Guid userId, CancellationToken token);

    Task<UserModel?> GetUserByExternalIdAsync(string externalId, CancellationToken token);

    Task<IList<UserModel>> GetUsersAsync(CancellationToken token);

    Task<IList<UserModel>> GetUsersForNewseletterAsync(CancellationToken token);

    Task<UserModel?> CreateUserAsync(UserModel user, CancellationToken token);
    
    Task<UpdateType> UpdateUserAsync(UserEditDto user, CancellationToken token);
    
    Task<DeleteType> DeleteUserAsync(Guid userId, CancellationToken token);
    
    Task<bool> CheckIfUserAdminAsync(Guid userId, CancellationToken token);
    
    Task SaveChangesAsync(CancellationToken token);
}