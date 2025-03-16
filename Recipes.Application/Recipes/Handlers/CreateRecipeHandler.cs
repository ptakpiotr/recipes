using Recipes.Application.Recipes.Commands;
using Recipes.Application.Recipes.DTO;
using Recipes.Application.Recipes.Services;

namespace Recipes.Application.Recipes.Handlers;

public class CreateRecipeHandler(IRecipeService service)
    : IRequestHandler<CreateRecipeCommand, OneOf<SuccessWithValue<RecipeReadDto>, Error>>
{
    public Task<OneOf<SuccessWithValue<RecipeReadDto>, Error>> Handle(CreateRecipeCommand request,
        CancellationToken cancellationToken)
    {
        return service.CreateRecipeAsync(request.Recipe, cancellationToken);
    }
}