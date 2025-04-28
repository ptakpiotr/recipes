using Recipes.Application.Recipes.Queries;
using Recipes.Application.Recipes.Services;

namespace Recipes.Application.Recipes.Handlers;

public class CheckOwnershipHandler(IRecipeService service)
    : IRequestHandler<CheckOwnershipQuery, OneOf<Success, Error>>
{
    public Task<OneOf<Success, Error>> Handle(CheckOwnershipQuery request, CancellationToken cancellationToken)
    {
        return service.CheckOwnershipAsync(request.UserId, request.RecipeId, cancellationToken);
    }
}