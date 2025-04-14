namespace Recipes.Application.Users.Services;

public interface IUserService
{
    Task<OneOf<SuccessWithValue<UserReadDto>, Error>> GetUserByIdAsync(Guid userId, CancellationToken token);

    Task<OneOf<SuccessWithValue<UserReadDto>, Error>> GetUserByExternalIdAsync(string externalId,
        CancellationToken token);

    Task<OneOf<SuccessWithValue<IReadOnlyList<UserReadDto>>, Error>> GetUsersForNewseletterAsync(
        CancellationToken token);
    
    Task<OneOf<SuccessWithValue<bool>, Error>> CheckIfUserAdminAsync(Guid userId, CancellationToken token);

    Task<OneOf<SuccessWithValue<IReadOnlyList<UserReadDto>>, Error>> GetAllUsersAsync(CancellationToken token);
    
    Task<OneOf<SuccessWithValue<IReadOnlyList<UserBasicReadDto>>, Error>> GetBasicsUsersAsync(CancellationToken token);

    Task<OneOf<SuccessWithValue<UserReadDto>, Error>> CreateUserAsync(UserCreateDto user, CancellationToken token);

    Task<OneOf<Success, Error>> UpdateUserAsync(UserEditDto user, CancellationToken token);

    Task<OneOf<Success, Error>> DeleteUserAsync(UserDeleteDto user, CancellationToken token);
}