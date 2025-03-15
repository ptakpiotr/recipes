using Recipes.Domain.Common.Enums;

namespace Recipes.Domain.Common.Results;

public record Error(ErrorType Type, string Message = "");
