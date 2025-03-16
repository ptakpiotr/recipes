using Recipes.Application.Recipes.DTO;

namespace Recipes.Application.Recipes.Commands;

public record CreateRatingCommand(RatingCreateDto Rating) : IRequest<OneOf<SuccessWithValue<RatingReadDto>, Error>>;