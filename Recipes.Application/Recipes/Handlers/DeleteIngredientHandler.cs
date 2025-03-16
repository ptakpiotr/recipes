using Recipes.Application.Recipes.Commands;
using Recipes.Application.Recipes.Services;
using Recipes.Domain.Common.ValueObjects;

namespace Recipes.Application.Recipes.Handlers;

public class DeleteIngredientHandler(IIngredientsService service)
    : IRequestHandler<DeleteIngredientCommand, OneOf<CommandStatus, Error>>
{
    public async Task<OneOf<CommandStatus, Error>> Handle(DeleteIngredientCommand request,
        CancellationToken cancellationToken)
    {
        var deleteResult = await service.DeleteIngredientAsync(request.Ingredient, request.UserId, cancellationToken)
            .ConfigureAwait(ConfigureAwaitOptions.None);

        var res = deleteResult.Match((_) => new CommandStatus(true), (_) => new CommandStatus(false));

        return res;
    }
}