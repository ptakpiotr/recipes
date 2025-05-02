using Recipes.Application.Recipes.Commands;
using Recipes.Application.Recipes.Services;

namespace Recipes.Application.Recipes.Handlers;

public class MassCreateRecipesHandler(IRecipeService service) : IRequestHandler<MassCreateRecipesCommand,  OneOf<Success, Error>>
{
    public Task<OneOf<Success, Error>> Handle(MassCreateRecipesCommand request, CancellationToken cancellationToken)
    {
        return service.MassCreateRecipesAsync(request.Recipes, cancellationToken);
    }
}