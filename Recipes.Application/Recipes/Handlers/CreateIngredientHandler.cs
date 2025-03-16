using Recipes.Application.Recipes.Commands;
using Recipes.Application.Recipes.DTO;
using Recipes.Application.Recipes.Services;

namespace Recipes.Application.Recipes.Handlers;

public class CreateIngredientHandler(IIngredientsService service) : IRequestHandler<CreateIngredientCommand, OneOf<SuccessWithValue<IngredientReadDto>, Error>>
{
    public Task<OneOf<SuccessWithValue<IngredientReadDto>, Error>> Handle(CreateIngredientCommand request, CancellationToken cancellationToken)
    {
        return service.CreateIngredientAsync(request.Ingredient, cancellationToken);
    }
}