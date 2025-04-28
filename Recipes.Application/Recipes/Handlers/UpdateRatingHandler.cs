using Recipes.Application.Recipes.Commands;
using Recipes.Application.Recipes.Services;
using Recipes.Domain.Common.ValueObjects;

namespace Recipes.Application.Recipes.Handlers;

public class UpdateRatingHandler(IRatingsService service)
    : IRequestHandler<UpdateRatingCommand, OneOf<CommandStatus, Error>>
{
    public async Task<OneOf<CommandStatus, Error>> Handle(UpdateRatingCommand request,
        CancellationToken cancellationToken)
    {
        var updateResult = await service.UpdateRatingAsync(request.Rating, request.UserId, cancellationToken)
            .ConfigureAwait(ConfigureAwaitOptions.None);

        var res = updateResult.Match((_) => new CommandStatus(true), (_) => new CommandStatus(false));

        return res;
    }
}