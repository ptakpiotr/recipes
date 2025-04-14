using Recipes.Application.Recipes.Commands;
using Recipes.Application.Recipes.DTO;
using Recipes.Application.Recipes.Services;
using Recipes.Domain.Common.Enums;
using Recipes.Domain.Common.ValueObjects;

namespace Recipes.Application.Recipes.Handlers;

public class DeleteRatingHandler(IRatingsService ratingsService)
    : IRequestHandler<DeleteRatingCommand, OneOf<CommandStatus, Error>>
{
    public async Task<OneOf<CommandStatus, Error>> Handle(DeleteRatingCommand request,
        CancellationToken cancellationToken)
    {
        var rating = await ratingsService.DeleteRecipeAsync(new RatingDeleteDto()
        {
            Id = request.Rating.Id
        }, request.UserId, cancellationToken).ConfigureAwait(false);

        if (rating.IsT0)
        {
            return new CommandStatus();
        }

        return new Error(ErrorType.OperationFailed);
    }
}