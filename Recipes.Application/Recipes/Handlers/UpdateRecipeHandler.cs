using Recipes.Application.Recipes.Commands;
using Recipes.Application.Recipes.Services;
using Recipes.Domain.Common.ValueObjects;

namespace Recipes.Application.Recipes.Handlers;

public class UpdateRecipeHandler(IRecipeService service)
    : IRequestHandler<UpdateRecipeCommand, OneOf<CommandStatus, Error>>
{
    public async Task<OneOf<CommandStatus, Error>> Handle(UpdateRecipeCommand request,
        CancellationToken cancellationToken)
    {
        var updateResult = await service.UpdateRecipeAsync(request.Recipe, request.UserId, cancellationToken)
            .ConfigureAwait(ConfigureAwaitOptions.None);

        var res = updateResult.Match((_) => new CommandStatus(true), (_) => new CommandStatus(false));

        return res;
    }
}