namespace Recipes.Application.Users.Queries;

public record GetUsersQuery : IRequest<OneOf<SuccessWithValue<IReadOnlyList<UserReadDto>>, Error>>;
