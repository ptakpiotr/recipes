using Recipes.Domain.Common.ValueObjects;

namespace Recipes.Application.Users.Commands;

public record DeleteUserCommand(UserDeleteDto User) : IRequest<OneOf<CommandStatus, Error>>, IValidate;