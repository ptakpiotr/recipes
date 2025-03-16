using Recipes.Application.Recipes.DTO;
using Recipes.Application.Recipes.Queries;
using Recipes.Application.Recipes.Services;

namespace Recipes.Application.Recipes.Handlers;

public class GetAllRecipesHandler(IRecipeService service)
    : IRequestHandler<GetAllRecipesQuery, OneOf<SuccessWithValue<IReadOnlyList<RecipeReadDto>>, Error>>
{
    public Task<OneOf<SuccessWithValue<IReadOnlyList<RecipeReadDto>>, Error>> Handle(GetAllRecipesQuery request,
        CancellationToken cancellationToken)
    {
        return service.GetAllRecipesAsync(cancellationToken);
    }
}