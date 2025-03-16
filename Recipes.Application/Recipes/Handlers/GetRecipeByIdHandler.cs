using Recipes.Application.Recipes.DTO;
using Recipes.Application.Recipes.Queries;
using Recipes.Application.Recipes.Services;

namespace Recipes.Application.Recipes.Handlers;

public class GetRecipeByIdHandler(IRecipeService service) : IRequestHandler<GetRecipeByIdQuery, OneOf<SuccessWithValue<RecipeReadDto>, Error>>
{
    public Task<OneOf<SuccessWithValue<RecipeReadDto>, Error>> Handle(GetRecipeByIdQuery request, CancellationToken cancellationToken)
    {
        return service.GetRecipeByIdAsync(request.RecipeId, cancellationToken);
    }
}
