using Recipes.Application.Recipes.Queries;
using Recipes.Application.Recipes.Services;

namespace Recipes.Application.Recipes.Handlers;

public class CheckRatingOwnershipHandler(IRatingsService service)
    : IRequestHandler<CheckRatingOwnershipQuery, OneOf<Success, Error>>
{
    public Task<OneOf<Success, Error>> Handle(CheckRatingOwnershipQuery request, CancellationToken cancellationToken)
    {
        return service.CheckOwnershipAsync(request.UserId, request.RatingId, cancellationToken);
    }
}