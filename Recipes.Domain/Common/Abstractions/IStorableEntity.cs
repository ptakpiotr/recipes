namespace Recipes.Domain.Common.Abstractions;

public interface IStorableEntity
{
    Guid Id { get; set; }
}