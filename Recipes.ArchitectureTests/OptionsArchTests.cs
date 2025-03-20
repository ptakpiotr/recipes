using System.ComponentModel.DataAnnotations;
using NetArchTest.Rules;
using Recipes.Infrastructure.Common.Options;
using Xunit;

namespace Recipes.InfrastructureTests;

public class OptionsArchTests
{
    [Fact]
    public void AllProperties_Should_Be_Required()
    {
        var allRequired = Types.InNamespace(typeof(S3Options).Namespace)
            .That().HaveNameMatching(".*Options")
            .GetTypes().Select(t =>
                t.GetProperties().All(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(RequiredAttribute))))
            .All(x => x);

        Assert.True(allRequired);
    }
}