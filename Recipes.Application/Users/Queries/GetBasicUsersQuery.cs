namespace Recipes.Application.Users.Queries;

public record GetBasicUsersQuery : IRequest<OneOf<SuccessWithValue<IReadOnlyList<UserBasicReadDto>>, Error>>;