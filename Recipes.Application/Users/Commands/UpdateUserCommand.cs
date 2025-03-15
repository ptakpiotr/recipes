using Recipes.Domain.Common.ValueObjects;

namespace Recipes.Application.Users.Commands;

public record UpdateUserCommand(UserEditDto User) : IRequest<OneOf<CommandStatus, Error>>;
