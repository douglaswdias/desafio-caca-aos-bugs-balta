using Balta.Domain.SharedContext.Extensions;
using Bogus;
using FluentAssertions;

namespace Balta.Domain.Test.SharedContext.Extensions;

public class StringExtensionsTests
{
    private readonly Faker _faker = new Faker();
    [Fact]
    public void ShouldGenerateBase64FromString()
    {
        var stringToTest = _faker.Lorem.Sentence();
        var result = stringToTest.ToBase64();

        result.Should().NotBeNull();
    }
}