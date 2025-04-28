namespace Recipes.Application.Recipes.Queries;

public record CheckOwnershipQuery(Guid UserId, Guid RecipeId) : IRequest<OneOf<Success, Error>>;