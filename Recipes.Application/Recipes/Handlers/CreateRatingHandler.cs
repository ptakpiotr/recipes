using Recipes.Application.Recipes.Commands;
using Recipes.Application.Recipes.DTO;
using Recipes.Application.Recipes.Services;

namespace Recipes.Application.Recipes.Handlers;

public class CreateRatingHandler(IRatingsService service) : IRequestHandler<CreateRatingCommand, OneOf<SuccessWithValue<RatingReadDto>, Error>>
{
    public Task<OneOf<SuccessWithValue<RatingReadDto>, Error>> Handle(CreateRatingCommand request, CancellationToken cancellationToken)
    {
        return service.CreateRatingForRecipeAsync(request.Rating, cancellationToken);
    }
}