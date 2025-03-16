using Recipes.Application.Recipes.Commands;
using Recipes.Application.Recipes.Services;
using Recipes.Domain.Common.ValueObjects;

namespace Recipes.Application.Recipes.Handlers;

public class UpdateIngredientHandler(IIngredientsService service)
    : IRequestHandler<UpdateIngredientCommand, OneOf<CommandStatus, Error>>
{
    public async Task<OneOf<CommandStatus, Error>> Handle(UpdateIngredientCommand request,
        CancellationToken cancellationToken)
    {
        var deleteResult = await service.UpdateIngredientAsync(request.Ingredient, request.UserId, cancellationToken)
            .ConfigureAwait(ConfigureAwaitOptions.None);

        var res = deleteResult.Match((_) => new CommandStatus(true), (_) => new CommandStatus(false));

        return res;
    }
}