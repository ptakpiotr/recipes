namespace Recipes.Application.Users.Queries;

public record CheckIfAdminQuery(Guid UserId) : IRequest<OneOf<SuccessWithValue<bool>, Error>>;