namespace Recipes.Application.Recipes.Queries;

public record CheckRatingOwnershipQuery(Guid UserId, Guid RatingId) : IRequest<OneOf<Success, Error>>;