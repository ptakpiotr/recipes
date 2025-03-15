namespace Recipes.Application.Users.Queries;

public record GetUserByIdQuery(Guid UserId) : IRequest<OneOf<SuccessWithValue<UserReadDto>, Error>>;